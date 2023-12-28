using Sugamta.API.DTOs.UserDTOs;

namespace Sugamta.API.Repository.Interface
{
    public interface IUser
    {
        List<UserDto> GetUsers();
        UserDto GetUser(string email);

        void CreateUser(UserDto userDto);
        bool DeleteUser(string email);




        void UpdateUser(int email, UserDto updatedUserDto);


    }
}
