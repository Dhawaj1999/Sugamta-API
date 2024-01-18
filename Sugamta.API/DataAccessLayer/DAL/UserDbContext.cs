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
        public DbSet<LinkGeneration> GeneratedRegisterLinks { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<PrimaryClient> PrimaryClients { get; set; }
        public DbSet<PrimaryClientDetails> PrimaryClientsDetails { get; set; }


        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDetails>().HasKey(u => u.Email);

            modelBuilder.Entity<UserDetails>()
                .HasOne<User>(u => u.User)
                .WithOne(u => u.UserDetails)
                .HasForeignKey<UserDetails>(u => u.Email)
                .HasPrincipalKey<User>(u => u.Email);

            modelBuilder.Entity<PrimaryClient>()
                .HasOne(pc => pc.Agency)
                .WithMany() // Assuming Agency has multiple PrimaryClients
                .HasForeignKey(pc => pc.AgencyEmail)
                .HasPrincipalKey(a => a.Email);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            // Intercept changes before saving to the database
            var usersToInsertIntoAgency = ChangeTracker.Entries<User>()
                .Where(entry => entry.State == EntityState.Added && entry.Entity.RoleId == 5)
                .Select(entry => new Agency
                {
                    // UserID = entry.Entity.UserID,
                    Email = entry.Entity.Email,
                    Name = entry.Entity.Name,
                    Password = entry.Entity.Password,
                    CreationDate = entry.Entity.CreationDate
                    // Add other properties as needed
                })
                .ToList();
            var result = base.SaveChanges();

            foreach (var agency in usersToInsertIntoAgency)
            {
                //agency.UserID = agency.UserID;
                Entry(agency).State = EntityState.Added;
            }
            base.SaveChanges();
            return result;
            // Continue with the normal save operation
            //return base.SaveChanges();
        }



    }
}
