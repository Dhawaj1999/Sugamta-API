using Models.Models;
using Models.Models.DTOs.SecondaryClientDetailsDTOs;

namespace Sugamta.API.Repository.Interface
{
    public interface ISecondaryClientDetails
    {
        List<SecondaryClientDetail> GetSecondaryClientDetailsList();
        SecondaryClientDetail GetSecondaryClientDetail(string email);
          void InsertSecondaryclientDetails(SecondaryClientDetailsDtos secondaryClientDetailsDtos);
       /* void InsertSecondaryclientDetails(SecondaryClientDetailsCreateOrUpdateDto secondaryClientDetailsDtos);*/
        void UpdateSecondaryClientDetails(SecondaryClientDetailsDtos secondaryClientDetailsDtos);
        void DeleteSecondaryClientDetails(string secondaryClientDetailsEmail);
       /* void InsertSecondaryclientDetails(SecondaryClientDetailsDtos secondaryClientDetailsDtos);*/
    }
}
