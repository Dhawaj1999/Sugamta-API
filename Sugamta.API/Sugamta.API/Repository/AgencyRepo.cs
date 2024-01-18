using DataAccessLayer.Data;
using Models.Models;
using Sugamta.API.Repository.Interface;

namespace Sugamta.API.Repository
{
    public class AgencyRepo:IAgency
    {
        private readonly UserDbContext _context;

        public AgencyRepo(UserDbContext context)
        {
            _context=context;
        }

        public List<Agency> GetAgencies()
        {
            return _context.Agencies.ToList();
        }

        public Agency GetAgencyByEmail(string Email)
        {
            return _context.Agencies.FirstOrDefault(x => x.Email == Email);
        }
    }
}
