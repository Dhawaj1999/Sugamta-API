using Models.Models;

namespace Sugamta.API.Repository.Interface
{
    public interface IAgency
    {
        List<Agency> GetAgencies();
        Agency GetAgencyByEmail(string Email);
    }
}
