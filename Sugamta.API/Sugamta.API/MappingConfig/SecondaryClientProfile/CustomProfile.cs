using Mapster;
using Models.Models;
using Models.Models.DTOs.SecondaryClientDTOs;

namespace Sugamta.API.MappingConfig.SecondaryClientProfile
{
    public class CustomProfile
    {
        public CustomProfile()
        {
            TypeAdapterConfig<SecondaryClient, SecondaryClientDto>.NewConfig().TwoWays();
            /*TypeAdapterConfig<SecondaryClient, SecondaryClientCreateDTOs>.NewConfig().TwoWays();*/
        }
    }
}
