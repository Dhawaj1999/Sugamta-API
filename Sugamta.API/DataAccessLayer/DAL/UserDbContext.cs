using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data
{
    public class UserDbContext:DbContext
    {
        public UserDbContext()
        {
            
        }

        public UserDbContext(DbContextOptions<UserDbContext>options):base(options)
        {

        }
        public DbSet<User>Users { get; set; }
        public DbSet<UserDetails> UsersDetails { get; set; }
        public DbSet<UserLoginHistory> UserLoginHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*  // modelBuilder.Entity<UserDetails>().HasNoKey();
              modelBuilder.Entity<UserDetails>().HasKey(u => u.Email);

              // Configure the relationship between UserDetails and User
              modelBuilder.Entity<UserDetails>()
                  .HasOne(u => u.User)
                  .WithOne()
                  .HasForeignKey<UserDetails>(u => u.Email);*/
            modelBuilder.Entity<UserDetails>().HasKey(u => u.Email);

            modelBuilder.Entity<UserDetails>()
                .HasOne<User>(u => u.User)
                .WithOne(u => u.UserDetails)
                .HasForeignKey<UserDetails>(u => u.Email)
                .HasPrincipalKey<User>(u => u.Email);

            base.OnModelCreating(modelBuilder);
        }
    }
}
