using Mapster;
using Models.Models;
using Sugamta.API.DTOs.UserDetailsDTOs;

namespace Sugamta.API.MappingConfig.UserDetailsProfile
{
    public class CustomProfile
    {
        public CustomProfile()
        {
            TypeAdapterConfig<UserDetails, UserDetailsDto>.NewConfig().TwoWays();
        }
    }
}
