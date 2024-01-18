using DataAccessLayer.Data;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models.Models.DTOs.PrimaryClientDTOs;
using Sugamta.API.DTOs.UserDetailsDTOs;
using Sugamta.API.Repository.Interface;

namespace Sugamta.API.Repository
{
    public class PrimaryClientRepo:IPrimaryClient
    {
        private readonly UserDbContext _context;

        public PrimaryClientRepo(UserDbContext context)
        {
            _context=context;
        }


        public List<PrimaryClient> GetPrimaryClient()
        {
            return _context.PrimaryClients.ToList();
        }
        public PrimaryClient GetPrimaryClientByEmail(string email)
        {
            return _context.PrimaryClients.FirstOrDefault(u => u.PrimaryClientEmail == email);
        }

        public void InsertPrimaryClient(PrimaryClientCreateDto primaryClientDto)
        {
            var createClient = primaryClientDto.Adapt<PrimaryClient>();
            _context.PrimaryClients.Add(createClient);
            _context.SaveChanges();
        }

        public void UpdatePrimaryClient(PrimaryClientUpdateDto primaryClientDto)
        {
            /* var updateClient = primaryClient.Adapt<PrimaryClient>();
             _context.PrimaryClients.Update(updateClient);
             _context.SaveChanges();*/
            var client = _context.PrimaryClients.Find(primaryClientDto.PrimaryClientEmail);
            if (client != null)
            {
                client.PrimaryClientName = primaryClientDto.PrimaryClientName;
                client.Password = primaryClientDto.Password;
                client.IsDeleted = primaryClientDto.IsDeleted;
                client.RoleId = primaryClientDto.RoleId;
                _context.SaveChanges();
            }
        }

        public void DeletePrimaryClient(PrimaryClient primaryClient)
        {
            var local = _context.Set<PrimaryClient>().Local.FirstOrDefault(entry => entry.PrimaryClientEmail.Equals(primaryClient.PrimaryClientEmail));

            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }

            _context.PrimaryClients.Remove(primaryClient);
            _context.SaveChanges();
        }
    }
}
