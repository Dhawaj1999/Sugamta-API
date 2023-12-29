using DataAccessLayer.Data;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Sugamta.API.DTOs.UserDetailsDTOs;
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

        public UserDetailsDto GetUserDetails(string email)
        {
           // return _context.UsersDetails.FirstOrDefault(user => user.Email == email);
            var userDetails = _context.UsersDetails.FirstOrDefault(user => user.Email == email);
            return userDetails.Adapt<UserDetailsDto>();
        }

        public void InsertUserDetails(UserDetailsDto userDetailsDto)
        {
            var userDetails = userDetailsDto.Adapt<UserDetails>();
            _context.UsersDetails.Add(userDetails);
            _context.SaveChanges();
        }

        public void UpdateUserDetails(UserDetailsDto userDetailsDto)
        {
            var userDetails = userDetailsDto.Adapt<UserDetails>();
            _context.UsersDetails.Update(userDetails);
            _context.SaveChanges();
        }

        public void DeleteUserDetails(UserDetails userDetails)
        {
            
            var local = _context.Set<UserDetails>().Local.FirstOrDefault(entry => entry.Email.Equals(userDetails.Email));

            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }

            _context.UsersDetails.Remove(userDetails);
            _context.SaveChanges();
        }
    }
}
