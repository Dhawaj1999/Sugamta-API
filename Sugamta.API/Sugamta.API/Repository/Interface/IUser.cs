using Models.Models;
using Models.Models.DTOs.UserDTOs;
using Sugamta.API.DTOs.UserDTOs;

namespace Sugamta.API.Repository.Interface
{
    public interface IUser
    {
        List<User> GetUsers();
        User GetUser(string email);

        void CreateUser(UserCreateDto userDto);
        bool DeleteUser(string email);

        void UpdateUserWithoutOtp(UserUpdateDto userDto);


        void UpdateUser(string? email, UserOtpDto updatedUserDto);


    }
}
