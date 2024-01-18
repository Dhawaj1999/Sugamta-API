using DataAccessLayer.Data;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models.Models.DTOs.SecondaryClientDetailsDTOs;
using Models.Models.DTOs.SecondaryClientDTOs;
using Sugamta.API.Repository.Interface;

namespace Sugamta.API.Repository
{
    public class SecondaryClientRepository : ISecondaryClient
    {
        private readonly UserDbContext _context;

        public SecondaryClientRepository(UserDbContext context)
        {
            _context = context;
        }

        public List<SecondaryClient> GetSecondaryClientsList()
        {
            return _context.SecondaryClients.ToList();
        }
        public SecondaryClient GetSecondaryClients(string email)
        {
            return _context.SecondaryClients.FirstOrDefault(sc => sc.SecondaryClientEmail == email);
        }
        public void InsertSecondaryClients(SecondaryClientDto secondaryClientDto)
        {
            var secondaryClients = secondaryClientDto.Adapt<SecondaryClient>();
            _context.SecondaryClients.Add(secondaryClients);
            _context.SaveChanges();
        }

        public void UpdateSecondaryClient(SecondaryClientDto secondaryClientDto)
        {
            var secondaryClients = secondaryClientDto.Adapt<SecondaryClient>();
            _context.SecondaryClients.Update(secondaryClients);
            _context.SaveChanges();
        }
        public void DeleteSecondaryClient(SecondaryClient secondaryClient)
        {
            var local = _context.Set<SecondaryClient>().Local.FirstOrDefault(entry => entry.SecondaryClientEmail.Equals(secondaryClient.SecondaryClientEmail));

            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }

            _context.SecondaryClients.Remove(secondaryClient);
            _context.SaveChanges();

        }

       

        /* public void InsertsecondaryClients(SecondaryClientCreateDTOs secondaryClientCreateDTOs)
         {
             var secondaryClients = secondaryClientCreateDTOs.Adapt<SecondaryClient>();
             _context.SecondaryClients.Add(secondaryClients);
             _context.SaveChanges();
         }*/
    }
}