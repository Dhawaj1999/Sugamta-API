using Microsoft.EntityFrameworkCore;
using Models;
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
        public DbSet<Role> Roles { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }  
        public DbSet<SecondaryClient> SecondaryClients { get; set; }    

        public DbSet<SecondaryClientDetail> SecondaryClientDetails { get; set; }
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




            modelBuilder.Entity<SecondaryClient>()
               .HasOne(sc => sc.PrimaryClient)
               .WithMany()
               .HasForeignKey(sc => sc.PrimaryClientEmail)
               .HasPrincipalKey(a => a.PrimaryClientEmail);


            modelBuilder.Entity<SecondaryClientDetail>()
             .HasOne(scd => scd.SecondaryClient)
             .WithMany()
             .HasForeignKey(scd => scd.SecondaryClientEmail)
             .HasPrincipalKey(a => a.SecondaryClientEmail);

        }

    }
}
