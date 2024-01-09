using Models.Models;
using Models.Models.DTOs.UserDTOs;
using Sugamta.API.DTOs.UserDTOs;

namespace Sugamta.API.Repository.Interface
{
    public interface IUser
    {
        List<User> GetUsers();
        User GetUser(string email);

        void CreateUser(UserDto userDto);
        bool DeleteUser(string email);




        void UpdateUser(string email, UserOtpDto updatedUserDto);


    }
}
