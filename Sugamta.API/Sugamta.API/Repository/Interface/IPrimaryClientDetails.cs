using Models.Models.DTOs.PrimaryClientDTOs;
using Models.Models;
using Models.Models.DTOs.PrimaryClientDetailsDTOs;

namespace Sugamta.API.Repository.Interface
{
    public interface IPrimaryClientDetails
    {
        PrimaryClientDetails GetPrimaryClientDetailsByEmail(string email);
        void InsertPrimaryClientDetails(PrimaryClientDetailsDto primaryClientDto);
        void UpdatePrimaryClientDetails(PrimaryClientDetailsDto primaryClientDto);
        void DeletePrimaryClientDetails(PrimaryClientDetails primaryClientDto);
    }
}
