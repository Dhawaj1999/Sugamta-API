using DataAccessLayer.Data;
using Models.Models;
using Sugamta.API.Repository.Interface;
using System.Linq;
using System.Linq.Expressions;

namespace Sugamta.API.Repository
{
    public class CountryRepo:ICountry
    {
        private readonly UserDbContext _context;

        public CountryRepo(UserDbContext context)
        {
            _context=context;
        }

        public List<Country> GetCountries()
        {
            return _context.Countries.ToList();
        }

        public Country GetCountryById(int countryId)
        {
            return _context.Countries.FirstOrDefault(u => u.CountryId == countryId);
        }

        /*public Country GetCountryById(Expression<Func<Country, bool>> filter)
        {
            return _context.Countries.FirstOrDefault(filter);
        }*/
    }
}
