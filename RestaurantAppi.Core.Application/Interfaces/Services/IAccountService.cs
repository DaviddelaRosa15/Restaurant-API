using RestaurantAppi.Core.Application.Dtos.Account;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<RegisterResponse> RegisterWaiterUserAsync(RegisterRequest request, string origin);
        Task SignOutAsync();
        Task<RegisterResponse> RegisterAdminUserAsync(RegisterRequest request, string origin);
        Task<JwtSecurityToken> GenerateJWToken(string userId);
        RefreshToken GenerateRefreshToken(string userId);

	}
}