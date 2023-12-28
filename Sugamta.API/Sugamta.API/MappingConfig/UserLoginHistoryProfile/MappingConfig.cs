using Mapster;
using Models.Models;
using Models.Models.DTOs.UserLoginHistoryDTOs;
using Sugamta.API.Repository.Interface;
using System.Linq.Expressions;

namespace Sugamta.API.MappingConfig.UserLoginHistoryProfile
{
    public class MappingConfig : IMappingConfig
    {
        private readonly IUnitOfWork _unitOfWork;

        public MappingConfig(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public List<UserLoginHistoryDto> MapUserLoginHistoryToUserLoginHistoryDto()
        {
            var _user = _unitOfWork.UserLoginHistory.GetAllLoginHistory().ToList();
            List<UserLoginHistoryDto> userLoginDto = _user.Adapt<List<UserLoginHistoryDto>>();

            return userLoginDto.ToList();
        }

        public List<UserLoginHistoryDto> MapSingleUserLoginHistoryDto(string email)
        {
            var user = _unitOfWork.UserLoginHistory.GetLoginHistoryByEmail(e => e.Email == email);
            List<UserLoginHistoryDto> mappedUser = user.Adapt<List<UserLoginHistoryDto>>();
            return mappedUser;
        }

        public UserLoginHistory MapCreateUserLoginHistoryDtoToUserLoginHistory(CreateUserLoginHistoryDto createUserLoginHistoryDto)
        {
            UserLoginHistory mappedLoginHistory = createUserLoginHistoryDto.Adapt<UserLoginHistory>();
            return mappedLoginHistory;
        }

        public CreateUserLoginHistoryDto MapUserToCreateUserLoginHistoryDto(User user)
        {
            CreateUserLoginHistoryDto mappedCreateUserLoginHistory = user.Adapt<CreateUserLoginHistoryDto>();
            return mappedCreateUserLoginHistory;
        }
    }
}
