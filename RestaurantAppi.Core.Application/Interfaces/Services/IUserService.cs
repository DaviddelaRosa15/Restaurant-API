using RestaurantAppi.Core.Application.Dtos.Account;
using RestaurantAppi.Core.Application.ViewModels.User;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
        Task<RegisterResponse> RegisterWaiterAsync(SaveUserViewModel vm, string origin);
        Task<RegisterResponse> RegisterAdminAsync(SaveUserViewModel vm, string origin);
        Task SignOutAsync();
    }
}