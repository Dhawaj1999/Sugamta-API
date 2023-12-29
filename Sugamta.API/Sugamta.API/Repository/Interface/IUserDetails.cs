using Models.Models;
using Sugamta.API.DTOs.UserDetailsDTOs;

namespace Sugamta.API.Repository.Interface
{
    public interface IUserDetails
    {
        UserDetails GetUserDetails(string email);
        void InsertUserDetails(UserDetailsDto userDetailsDto);
        void UpdateUserDetails(UserDetailsDto userDetailsDto);
        void DeleteUserDetails(UserDetails userDetails);
    }
}
