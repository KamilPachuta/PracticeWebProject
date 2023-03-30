using Microsoft.AspNetCore.Authorization;
using RestaurantAPI.Entities;
using System.Net;
using System.Security.Claims;

namespace RestaurantAPI.Authorization
{
    public class CreatedRestaurantsRequirementHandler : AuthorizationHandler<CreatedRestaurantsRequirement>
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly ILogger<CreatedRestaurantsRequirementHandler> _logger;

        public CreatedRestaurantsRequirementHandler(RestaurantDbContext dbContext, ILogger<CreatedRestaurantsRequirementHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;

        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedRestaurantsRequirement requirement)
        {
            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var createdRestaurantsCount = _dbContext.Restaurants.Count(r => r.CreatedById == userId);

            if ( createdRestaurantsCount >= requirement.MinimumRestaurantsCreated )
            {
                _logger.LogInformation("Authorization successed");
                context.Succeed(requirement);
            }
            else
            {
                _logger.LogInformation("Authorization failed");
            }

            return Task.CompletedTask;
        }
    }
}
