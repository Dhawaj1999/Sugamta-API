using Models.Models;
using Models.Models.DTOs.UserLoginHistoryDTOs;

namespace Sugamta.API.MappingConfig.UserLoginHistoryProfile
{
    public interface IMappingConfig
    {
        List<UserLoginHistoryDto> MapUserLoginHistoryToUserLoginHistoryDto();
        List<UserLoginHistoryDto> MapSingleUserLoginHistoryDto(string email);
        UserLoginHistory MapCreateUserLoginHistoryDtoToUserLoginHistory(CreateUserLoginHistoryDto createUserLoginHistoryDto);
        CreateUserLoginHistoryDto MapUserToCreateUserLoginHistoryDto(User user);
    }
}
