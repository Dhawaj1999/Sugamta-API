using Mapster;
using Models.Models.DTOs.PrimaryClientDTOs;
using Models.Models;
using Models.Models.DTOs.PrimaryClientDetailsDTOs;

namespace Sugamta.API.MappingConfig.PrimaryClientDetailsProfile
{
    public class CustomProfile
    {
        public CustomProfile()
        {
            TypeAdapterConfig<PrimaryClientDetails, PrimaryClientDetailsDto>.NewConfig().TwoWays();
        }
    }
}
