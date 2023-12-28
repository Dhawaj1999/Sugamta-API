
using DataAccessLayer.Data;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Sugamta.API.DTOs.UserDTOs;
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

        public List<UserDto> GetUsers()
        {
            var users = _context.Users.ToList();
            return users.Adapt<List<UserDto>>(); // Using Mapster for mapping
        }

        public UserDto GetUser(string email)
        {
            var user = _context.Users.Find(email);
            return user.Adapt<UserDto>(); // Using Mapster for mapping
        }

        public void CreateUser(UserDto userDto)
        {
            var user = userDto.Adapt<User>(); // Using Mapster for mapping
            _context.Users.Add(user);
            _context.SaveChanges();
        }


        public void UpdateUser(int email, UserDto updatedUserDto)
        {
            var user = _context.Users.Find(email);

            if (user != null)
            {
                user.Name = updatedUserDto.Name;
                user.Password = BCrypt.Net.BCrypt.HashPassword(updatedUserDto.Password);
              

                _context.SaveChanges();
            }
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
