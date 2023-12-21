using Microsoft.EntityFrameworkCore;
using Models.Models;
namespace DataAccessLayer.DAL
{
    public  class UserDbContext:DbContext
    {

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserLoginHistory> UserLoginHistory { get; set; }
    }
}
