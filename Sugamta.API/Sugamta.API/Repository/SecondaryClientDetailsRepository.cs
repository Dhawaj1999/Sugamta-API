using DataAccessLayer.Data;
using Mapster;
using Models.Models;
using Models.Models.DTOs.SecondaryClientDetailsDTOs;
using Sugamta.API.Repository.Interface;

namespace Sugamta.API.Repository
{
    public class SecondaryClientDetailsRepository : ISecondaryClientDetails
    {
        private readonly UserDbContext _context;
        public SecondaryClientDetailsRepository(UserDbContext context)
        {
            _context = context;
        }

        public List<SecondaryClientDetail> GetSecondaryClientDetailsList()
        {
            return _context.SecondaryClientDetails.ToList();
        }

        public SecondaryClientDetail GetSecondaryClientDetail(string email)
        {
            return _context.SecondaryClientDetails.FirstOrDefault(scd => scd.SecondaryClientEmail == email);
        }

       /* public void InsertSecondaryclientDetails(SecondaryClientDetailsDtos secondaryClientDetailsDtos)
        {
            var secondClientDetails = secondaryClientDetailsDtos.Adapt<SecondaryClientDetail>();
            _context.SecondaryClientDetails.Add(secondClientDetails);
        }*/


        public void InsertSecondaryclientDetails(SecondaryClientDetailsDtos secondaryClientDetailsDtos)
        {
          var secondClientDetails = secondaryClientDetailsDtos.Adapt<SecondaryClientDetail>();
            _context.SecondaryClientDetails.Add(secondClientDetails);
            _context.SaveChanges();
        }

        public void UpdateSecondaryClientDetails(SecondaryClientDetailsDtos secondaryClientDetailsDtos)
        {
            var secondaryClientDetails = secondaryClientDetailsDtos.Adapt<SecondaryClientDetail>();
            _context.SecondaryClientDetails.Update(secondaryClientDetails);
            _context.SaveChanges();
        }
        public void DeleteSecondaryClientDetails(string secondaryClientDetailsEmail)
        {
            var secondaryClientDetails = _context.SecondaryClientDetails
                .FirstOrDefault(scd => scd.SecondaryClientEmail == secondaryClientDetailsEmail);

            if (secondaryClientDetails != null)
            {
                _context.SecondaryClientDetails.Remove(secondaryClientDetails);
                _context.SaveChanges();
            }
            else
            {
                
                throw new Exception($"Secondary client details with email {secondaryClientDetailsEmail} not found.");
            }
        }

    }
}