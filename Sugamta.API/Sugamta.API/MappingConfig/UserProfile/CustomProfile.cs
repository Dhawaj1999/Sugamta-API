using Mapster;
using Models.Models;
using Sugamta.API.DTOs.UserDTOs;

namespace Sugamta.API.MappingConfig.UserProfile
{
    public class CustomProfile
    {

        public CustomProfile()
        {
            TypeAdapterConfig<User, UserDto>.NewConfig().TwoWays();

        }

    }
}
