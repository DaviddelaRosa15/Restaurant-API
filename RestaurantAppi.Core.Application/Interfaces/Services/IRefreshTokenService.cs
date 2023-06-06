using RestaurantAppi.Core.Application.Dtos.Account;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.Interfaces.Services
{
    public interface IRefreshTokenService
    {
		Task Add(RefreshToken token);
		Task<List<RefreshToken>> GetAllViewModel();
		Task<RefreshToken> GetByToken(string token);
		Task Update(RefreshToken token, int id);


	}
}