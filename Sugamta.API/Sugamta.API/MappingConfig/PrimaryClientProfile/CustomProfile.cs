using Mapster;
using Models.Models;
using Models.Models.DTOs.PrimaryClientDTOs;
using Sugamta.API.DTOs.UserDetailsDTOs;

namespace Sugamta.API.MappingConfig.PrimaryClientProfile
{
    public class CustomProfile
    {
        public CustomProfile()
        {
            TypeAdapterConfig<PrimaryClient, PrimaryClientDto>.NewConfig().TwoWays();
           // TypeAdapterConfig<PrimaryClient, PrimaryClientCreateOrUpdateDto>.NewConfig().TwoWays();
        }
    }
}
