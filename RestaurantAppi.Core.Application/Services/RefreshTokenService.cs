using AutoMapper;
using Microsoft.AspNetCore.Http;
using RestaurantAppi.Core.Application.Dtos.Account;
using RestaurantAppi.Core.Application.Helpers;
using RestaurantAppi.Core.Application.Interfaces.Repositories;
using RestaurantAppi.Core.Application.Interfaces.Services;
using RestaurantAppi.Core.Application.ViewModels.Tables;
using RestaurantAppi.Core.Application.ViewModels.User;
using RestaurantAppi.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAppi.Core.Application.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshRepository;
        private readonly IMapper _mapper;

        public RefreshTokenService(IRefreshTokenRepository refresRepository,IMapper mapper)
        {
            _refreshRepository = refresRepository;            
            _mapper = mapper;
        }

        public async Task Add(Dtos.Account.RefreshToken token)
        {
           Domain.Entities.RefreshToken refresh = _mapper.Map<Domain.Entities.RefreshToken>(token);
            
            await _refreshRepository.AddAsync(refresh);
        }

		public async Task<List<Dtos.Account.RefreshToken>> GetAllViewModel()
		{
			var entityList = await _refreshRepository.GetAllAsync();

			return _mapper.Map<List<Dtos.Account.RefreshToken>>(entityList);
		}

		public async Task<Dtos.Account.RefreshToken> GetByToken(string token)
		{
            var list = await GetAllViewModel();
            var refresh = list.FirstOrDefault(x => x.Token == token);

			return refresh;
		}

		public async Task Update(Dtos.Account.RefreshToken token, int id)
		{
			Domain.Entities.RefreshToken entity = _mapper.Map<Domain.Entities.RefreshToken>(token);

			await _refreshRepository.UpdateAsync(entity, id);
		}
	}
}
