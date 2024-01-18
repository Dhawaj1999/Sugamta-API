using Models.Models;
using Models.Models.DTOs.SecondaryClientDTOs;

namespace Sugamta.API.Repository.Interface
{

    public interface ISecondaryClient
    {
        List<SecondaryClient> GetSecondaryClientsList();
        SecondaryClient GetSecondaryClients(string email);
        void InsertSecondaryClients(SecondaryClientDto secondaryClientDto);
          
        void UpdateSecondaryClient(SecondaryClientDto secondaryClientDto);
           
        void DeleteSecondaryClient(SecondaryClient secondaryClient);
       
    }
    
}
