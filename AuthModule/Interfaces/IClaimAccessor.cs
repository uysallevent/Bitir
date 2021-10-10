using AuthModule.Dtos;

namespace AuthModule.Interfaces
{
    public interface IClaimAccessor
    {
        void SetClaims(string token);
        Claims Claims { get; set; }
    }
}
