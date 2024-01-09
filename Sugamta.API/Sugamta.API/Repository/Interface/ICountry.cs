using Models.Models;
using System.Linq.Expressions;

namespace Sugamta.API.Repository.Interface
{
    public interface ICountry
    {
        //Country GetCountryById(Expression<Func<Country, bool>> filter);
        //Task<List<Country>> GetCountriesAsync();
        List<Country> GetCountries();
        Country GetCountryById(int countryId);
    }
}
