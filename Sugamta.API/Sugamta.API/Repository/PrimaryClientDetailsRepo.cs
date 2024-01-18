using DataAccessLayer.Data;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models.Models.DTOs.PrimaryClientDetailsDTOs;
using Sugamta.API.DTOs.UserDetailsDTOs;
using Sugamta.API.Repository.Interface;

namespace Sugamta.API.Repository
{
    public class PrimaryClientDetailsRepo:IPrimaryClientDetails
    {
        private readonly UserDbContext _context;

        public PrimaryClientDetailsRepo(UserDbContext context)
        {
            _context=context;
        }

        public PrimaryClientDetails GetPrimaryClientDetailsByEmail(string email)
        {
            return _context.PrimaryClientsDetails.FirstOrDefault(u => u.PrimaryClientEmail == email);
        }

        public void InsertPrimaryClientDetails(PrimaryClientDetailsDto primaryClientDto)
        {
            var clientDetails = primaryClientDto.Adapt<PrimaryClientDetails>();
            _context.PrimaryClientsDetails.Add(clientDetails);
            _context.SaveChanges();
        }

        public void UpdatePrimaryClientDetails(PrimaryClientDetailsDto primaryClientDto)
        {
            var clientDetails = primaryClientDto.Adapt<PrimaryClientDetails>();
            _context.PrimaryClientsDetails.Update(clientDetails);
            _context.SaveChanges();
        }

        public void DeletePrimaryClientDetails(PrimaryClientDetails primaryClientDto)
        {
            var local = _context.Set<PrimaryClientDetails>().Local.FirstOrDefault(entry => entry.PrimaryClientEmail.Equals(primaryClientDto.PrimaryClientEmail));

            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }

            _context.PrimaryClientsDetails.Remove(primaryClientDto);
            _context.SaveChanges();
        }

    }
}
