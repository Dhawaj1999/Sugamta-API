using Models.Models;

namespace Sugamta.API.Repository.Interface
{
    public interface IUser
    {
        List<User> GetUsers();
        User GetUser(string email);

        void CreateUser(User user);
        bool UpdateUser(string email, User user);

        bool DeleteUser(string email);

    }
}
