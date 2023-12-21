using Models.Models;

namespace Sugamta.API.Repository.Interface
{
    public interface IUserDetails
    {
        UserDetails GetUserDetails(string email);
        void InsertUserDetails(UserDetails userDetails);
        void UpdateUserDetails(UserDetails userDetails);
        void DeleteUserDetails(UserDetails userDetails);
    }
}
