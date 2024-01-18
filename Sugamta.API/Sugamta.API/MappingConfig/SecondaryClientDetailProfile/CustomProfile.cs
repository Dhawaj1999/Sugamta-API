using Mapster;
using Models.Models;
using Models.Models.DTOs.SecondaryClientDetailsDTOs;
using Models.Models.DTOs.SecondaryClientDTOs;

namespace Sugamta.API.MappingConfig.SecondaryClientDetailProfile
{
    public class CustomProfile
    {
        public CustomProfile()
        {
            TypeAdapterConfig<SecondaryClientDetail, SecondaryClientDetailsDtos>.NewConfig().TwoWays();
        }

    }
}
