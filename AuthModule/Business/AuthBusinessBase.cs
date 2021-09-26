using AuthModule.Dto;
using AuthModule.Dtos;
using AuthModule.Entities;
using AuthModule.Interfaces;
using AuthModule.Security.Hashing;
using AuthModule.Security.JWT;
using AuthModule.Validations;
using BaseModule.Business;
using Bitir.Data.Model.Dtos;
using Core.Aspects.Autofac.Validation;
using Core.DataAccess;
using Core.DataAccess.EntityFramework.Interfaces;
using Core.Utilities.Results;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthModule.Business
{
    public class AuthBusinessBase : BusinessBase<UserAccount>, IAuthBusinessBase<UserAccount>
    {
        private IRepository<UserAccount> _userAccountRepository;
        private IRepository<UserToken> _userTokenRepository;
        private IUnitOfWork _uow;
        private readonly ITokenHelper _tokenHelper;
        private readonly IConfiguration _configuration;

        public AuthBusinessBase(
            IRepository<UserAccount> userAccountRepository,
            IRepository<UserToken> userTokenRepository,
            IUnitOfWork uow,
            ITokenHelper tokenHelper,
            IConfiguration configuration) : base(userAccountRepository, uow)
        {
            _userAccountRepository = userAccountRepository;
            _userTokenRepository = userTokenRepository;
            _uow = uow;
            _tokenHelper = tokenHelper;
            _configuration = configuration;
        }

        private AccessToken CreateAccessToken(UserAccount user, List<OperationClaim> list)
        {
            var accessToken = _tokenHelper.CreateToken(user, list);
            return accessToken;
        }

        [ValidationAspect(typeof(UserAccountValidationRules))]
        public override async Task<ResponseWrapper<UserAccount>> InsertAsync(UserAccount entity)
        {
            var userCheck = await _userAccountRepository.GetAsync(x => (x.Username == entity.Username) || (x.Email == entity.Email));
            if (userCheck != null)
            {
                throw new Exception("This username already exists in the system");
            }
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(entity.Password, out passwordHash, out passwordSalt);
            entity.PasswordHash = passwordHash;
            entity.PasswordSalt = passwordSalt;
            await _userAccountRepository.AddAsync(entity);
            var result = await _uow.SaveChangesAsync();

            return new ResponseWrapper<UserAccount>(entity);
        }

        public async Task<ResponseWrapper<AccessToken>> Login(LoginDto loginDto)
        {
            var user = await _userAccountRepository.GetAsync(x => (x.Username == loginDto.Username) || (x.Email == loginDto.Username));
            if (user == null)
            {
                throw new Exception("Username could not be found");
            }

            if (!HashingHelper.VerifyPasswordHash(loginDto.Password.Trim(), user.PasswordHash, user.PasswordSalt))
            {
                throw new Exception("Incorrect password !!");
            }

            var accessToken = CreateAccessToken(user, new List<OperationClaim> { new OperationClaim { Id = user.Id, Name = user.Username } });
            var sqlServerDatetime = DateTime.Now;
            int.TryParse(_configuration.GetSection("TokenOptions.RefreshTokenExpiration").Value, out int expirationDate);

            var userToken = await _userTokenRepository.GetAsync(x => x.Id == user.Id && x.Status != 1);
            if (userToken != null)
            {
                await _userTokenRepository.AddAsync(
                new UserToken
                {
                    UserId = user.Id,
                    RefreshTokenExpirationDate = sqlServerDatetime.AddMinutes(expirationDate),
                    RefreshToken = accessToken.RefreshToken,
                    Status = 1
                });
                await _uow.SaveChangesAsync();
            }

            return new ResponseWrapper<AccessToken>(
                new AccessToken
                {
                    Token = accessToken.Token,
                    Expiration = accessToken.Expiration,
                    RefreshToken = accessToken.RefreshToken
                }
                );
        }

        public async Task<ResponseWrapper<AccessToken>> RefreshTokenLogin(int userId, string refreshToken)
        {
            AccessToken accessToken;
            var userToken = await _userTokenRepository.GetAsync(x => x.Id == userId && x.RefreshToken == refreshToken);
            var sqlServerDatetime = DateTime.Now;
            if (userToken != null && userToken.RefreshTokenExpirationDate < sqlServerDatetime)
            {
                accessToken = CreateAccessToken(userToken.UserAccount, new List<OperationClaim> { new OperationClaim { Id = userToken.UserAccount.Id, Name = userToken.UserAccount.Username } });
            }
            else
            {
                return new ResponseWrapper<AccessToken>(null);
            }

            return new ResponseWrapper<AccessToken>(accessToken);
        }
    }
}