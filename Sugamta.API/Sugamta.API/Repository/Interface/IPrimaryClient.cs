using Models.Models;
using Models.Models.DTOs.PrimaryClientDTOs;
using Sugamta.API.DTOs.UserDetailsDTOs;

namespace Sugamta.API.Repository.Interface
{
    public interface IPrimaryClient
    {
        List<PrimaryClient> GetPrimaryClient();
        PrimaryClient GetPrimaryClientByEmail(string email);
        void InsertPrimaryClient(PrimaryClientCreateDto primaryClientDto);
        void UpdatePrimaryClient(PrimaryClientUpdateDto primaryClientDto);
        void DeletePrimaryClient(PrimaryClient primaryClient);
    }
}
