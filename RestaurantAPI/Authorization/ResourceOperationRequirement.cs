using Microsoft.AspNetCore.Authorization;

namespace RestaurantAPI.Authorization
{

    public enum ResourceOperation
    {
        Create = 0,
        Read =1,
        Update = 2,
        Delete = 3,
    }

    public class ResourceOperationRequirement : IAuthorizationRequirement
    {
        public ResourceOperation ResourceOperation { get; }

        public ResourceOperationRequirement(ResourceOperation resourceOperation)
        {
            ResourceOperation = resourceOperation;
        }
    }
}
