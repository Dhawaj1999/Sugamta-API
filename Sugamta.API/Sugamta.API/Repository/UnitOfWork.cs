using DataAccessLayer.DAL;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Sugamta.API.Repository.Interface;

namespace Sugamta.API.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly UserDbContext _context;
        public IUser user { get; private set; }
        public IUserLoginHistory UserLoginHistory { get; private set; }
        public UnitOfWork(UserDbContext context)
        {

            _context = context;
            user = new UserRepo(_context);
            UserLoginHistory = new UserLoginHistoryRepo(_context);
        }
        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
