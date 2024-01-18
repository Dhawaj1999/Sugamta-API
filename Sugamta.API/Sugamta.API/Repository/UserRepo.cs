
using DataAccessLayer.Data;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models.Models.DTOs.UserDTOs;
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

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
            //var users = _context.Users.ToList();
            //return users.Adapt<List<UserDto>>(); // Using Mapster for mapping
        }

        public User GetUser(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
           // var user = _context.Users.Find(email);
           // return user.Adapt<UserDto>(); // Using Mapster for mapping
        }

        public void CreateUser(UserCreateDto userDto)
        {
            var user = userDto.Adapt<User>(); // Using Mapster for mapping
            _context.Users.Add(user);
            _context.SaveChanges();
        }


        public void UpdateUserWithoutOtp(UserUpdateDto updatedUserDto)
        {
            var user = _context.Users.Find(updatedUserDto.Email);

            if (user != null)
            {
                user.Name = updatedUserDto.Name;
                user.Password = BCrypt.Net.BCrypt.HashPassword(updatedUserDto.Password);
                user.IsDeleted = updatedUserDto.IsDeleted;
                user.RoleId = updatedUserDto.RoleId;

                _context.SaveChanges();
            }
        }

        public void UpdateUser(string email, UserOtpDto updatedUserDto)
        {
            var user = _context.Users.Find(email);

            if (user != null)
            {
                user.OTP = updatedUserDto.OTP;
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
