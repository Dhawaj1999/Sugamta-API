using Models.Models;
using Sugamta.API.DTOs.UserDTOs;

namespace Sugamta.API.Repository.Interface
{
    public interface IUser
    {
        List<User> GetUsers();
        User GetUser(string email);

        void CreateUser(UserDto userDto);
        bool DeleteUser(string email);




        void UpdateUser(int email, UserDto updatedUserDto);


    }
}
