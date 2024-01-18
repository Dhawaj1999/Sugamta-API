using DataAccessLayer.Data;
using Microsoft.AspNetCore.Routing;
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
        public IUserDetails UserDetails { get; private set; }
        public IRole Role { get; private set; }
        public ILinkGenerate LinkGenerate { get; private set; }
        public ICountry Country { get; private set; }
        public IState State { get; private set; }
        public ISecondaryClient SecondaryClient { get; private set; }   
        public ISecondaryClientDetails SecondaryClientDetail { get; private set; }
        public UnitOfWork(UserDbContext context)
        {

            _context = context;
            user = new UserRepo(_context);
            UserDetails = new UserDetailsRepository(_context);
            UserLoginHistory = new UserLoginHistoryRepo(_context);
            Role = new RoleRepo(_context);
            LinkGenerate = new LinkGenerateRepo(_context);
            Country =new CountryRepo(_context);
            State=new StateRepo(_context);
            SecondaryClient = new SecondaryClientRepository(_context);
           // SecondaryClient=new SecondaryClientRepository(_context);
           SecondaryClientDetail=new SecondaryClientDetailsRepository(_context);
        }
        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
