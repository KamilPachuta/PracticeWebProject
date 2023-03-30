using Microsoft.AspNetCore.Authorization;

namespace RestaurantAPI.Authorization
{
    public class CreatedRestaurantsRequirement : IAuthorizationRequirement
    {

        public CreatedRestaurantsRequirement(int minimumRestaurantsCreated)
        {
            MinimumRestaurantsCreated = minimumRestaurantsCreated;
        }

        public int MinimumRestaurantsCreated { get; set; }

    }
}
