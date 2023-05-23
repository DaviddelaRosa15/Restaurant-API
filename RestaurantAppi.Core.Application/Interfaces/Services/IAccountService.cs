using RestaurantAppi.Core.Application.Dtos.Account;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<RegisterResponse> RegisterWaiterUserAsync(RegisterRequest request, string origin);
        Task SignOutAsync();
        Task<RegisterResponse> RegisterAdminUserAsync(RegisterRequest request, string origin);
    }
}