using AuthModule.Dto;
using AuthModule.Dtos;
using AuthModule.Interfaces;
using AuthModule.Security.JWT;
using AuthModule.Validations;
using BaseModule.Business;
using Bitir.Data.Model.Dtos;
using Core.Aspects.Autofac.Validation;
using Core.DataAccess;
using Core.DataAccess.EntityFramework.Interfaces;
using Core.Exceptions;
using Core.Security.Hashing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Module.Shared.Entities.AuthModuleEntities;
using Module.Shared.Entities.SalesModuleEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthModule.Business
{
    public class AuthBusinessBase : BusinessBase<UserAccount>, IAuthBusinessBase<UserAccount>
    {
        private IRepository<UserAccount> _userAccountRepository;
        private IRepository<UserToken> _userTokenRepository;
        private IRepository<Store> _storeRepository;
        private IRepository<Store_UserAccount> _storeUserAccoutRepository;
        private IUnitOfWork _uow;
        private readonly ITokenHelper _tokenHelper;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public AuthBusinessBase(
            IRepository<UserAccount> userAccountRepository,
            IRepository<UserToken> userTokenRepository,
            IRepository<Store> storeRepository,
            IRepository<Store_UserAccount> storeUserAccoutRepository,
            IUnitOfWork uow,
            ITokenHelper tokenHelper,
            IConfiguration configuration,
            ILoggerFactory logger) : base(userAccountRepository, uow)
        {
            _userAccountRepository = userAccountRepository;
            _userTokenRepository = userTokenRepository;
            _storeRepository = storeRepository;
            _storeUserAccoutRepository = storeUserAccoutRepository;
            _uow = uow;
            _tokenHelper = tokenHelper;
            _configuration = configuration;
            _logger = logger.CreateLogger<AuthBusinessBase>();
        }

        private AccessToken CreateAccessToken(UserAccount user, List<OperationClaim> list)
        {
            var accessToken = _tokenHelper.CreateToken(user, list);
            return accessToken;
        }

        [ValidationAspect(typeof(UserAccountValidationRules))]
        public async Task<ResponseWrapper<AccessToken>> Register(UserAccount entity)
        {
            var userCheck = await _userAccountRepository.GetAsync(x => (x.Username == entity.Username) || (x.Email == entity.Email));
            if (userCheck != null)
            {
                throw new BadRequestException("Bu kullanıcı adı/e-posta sistemde kayıtlı");
            }
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(entity.Password, out passwordHash, out passwordSalt);
            entity.PasswordHash = passwordHash;
            entity.PasswordSalt = passwordSalt;
            await _userAccountRepository.AddAsync(entity);
            if (entity.AccountTypeId == Module.Shared.AccountTypeEnum.vendor)
            {
                var store = new Store
                {
                    Name = "Bitir Dükkanım"
                };
                await _storeRepository.AddAsync(store);
                await _storeUserAccoutRepository.AddAsync(
                    new Store_UserAccount
                    {
                        StoreId = store.Id,
                        UserId = entity.Id,
                        Store = store,
                        UserAccount = entity
                    });
            }

            var result = await _uow.SaveChangesAsync();
            if (result < 1)
            {
                throw new BadRequestException("Kullanıcı kayıt edilemedi");
            }
            AccessToken accessToken = await CreateToken(entity);
            return new ResponseWrapper<AccessToken>(accessToken);
        }

        public override Task<ResponseWrapper<UserAccount>> UpdateAsync(UserAccount entity)
        {
            if (!string.IsNullOrEmpty(entity.Password))
            {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(entity.Password, out passwordHash, out passwordSalt);
                entity.PasswordHash = passwordHash;
                entity.PasswordSalt = passwordSalt;
            }
            return base.UpdateAsync(entity);
        }

        public async Task<ResponseWrapper<AccessToken>> Login(LoginDto loginDto)
        {
            var entity = await _userAccountRepository.GetAsync(x => (x.Username == loginDto.Username) || (x.Email == loginDto.Username));
            if (entity == null)
            {
                _logger.LogError("Username could not be found");
                throw new BadRequestException("Kullanıcı adı bulunamadı");
            }

            if (!HashingHelper.VerifyPasswordHash(loginDto.Password.Trim(), entity.PasswordHash, entity.PasswordSalt))
            {
                _logger.LogError("Incorrect password");
                throw new BadRequestException("Kullanıcı adı/şifre hatalı");
            }

            AccessToken accessToken = await CreateToken(entity);
            return new ResponseWrapper<AccessToken>(accessToken);
        }

        private async Task<AccessToken> CreateToken(UserAccount entity)
        {
            var userStore = await _storeUserAccoutRepository.GetAsync(x => x.UserId == entity.Id);
            var accessToken = CreateAccessToken(entity, new List<OperationClaim> {
                new OperationClaim {
                    Id = entity.Id,
                    Name = entity.Username,
                    Email = entity.Email,
                    Phone=entity.Phone,
                    StoreId=userStore?.StoreId
                } });
            var sqlServerDatetime = DateTime.Now;
            int.TryParse(_configuration.GetSection("TokenOptions.RefreshTokenExpiration").Value, out int expirationDate);

            var userToken = await _userTokenRepository.GetAsync(x => x.Id == entity.Id && x.Status != Core.Enums.Status.Active);
            if (userToken != null)
            {
                await _userTokenRepository.AddAsync(
                new UserToken
                {
                    UserId = entity.Id,
                    RefreshTokenExpirationDate = sqlServerDatetime.AddMinutes(expirationDate),
                    RefreshToken = accessToken.RefreshToken,
                });
                await _uow.SaveChangesAsync();
            }

            return accessToken;
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