using DataAccessLayer.Data;
using Models.Models;
using Sugamta.API.Repository.Interface;

namespace Sugamta.API.Repository
{
    public class UserDetailsRepository : IUserDetails
    {
        private readonly UserDbContext _context;

        public UserDetailsRepository(UserDbContext context)
        {
            _context=context;
        }       

        public UserDetails GetUserDetails(string email)
        {
            return _context.UsersDetails.FirstOrDefault(user => user.Email == email);
        }

        public void InsertUserDetails(UserDetails userDetails)
        {
           _context.UsersDetails.Add(userDetails);
            _context.SaveChanges();
        }

        public void UpdateUserDetails(UserDetails userDetails)
        {
            _context.UsersDetails.Update(userDetails);
            _context.SaveChanges();
        }

        public void DeleteUserDetails(UserDetails userDetails)
        {
            _context.UsersDetails.Remove(userDetails);
            _context.SaveChanges();
        }
    }
}
