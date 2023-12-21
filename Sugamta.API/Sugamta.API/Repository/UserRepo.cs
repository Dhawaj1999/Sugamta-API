using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Sugamta.API.Repository.Interface;

namespace Sugamta.API.Repository
{
    public class UserRepo : IUser
    {
        private readonly UserDbContext _context;
        public UserRepo(UserDbContext context)
        {
            _context = context;
        }
        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUser(string email)
        {
            return _context.Users.Find(email);
        }

        public void CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public bool UpdateUser(string email, User user)
        {
            if (email != user.Email)
            {
                return false;
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(email))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public bool DeleteUser(string email)
        {
            var user = _context.Users.Find(email);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }

        private bool UserExists(string email)
        {
            return _context.Users.Any(e => e.Email == email);
        }
    }
}
