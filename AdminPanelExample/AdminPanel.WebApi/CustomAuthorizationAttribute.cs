using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using AdminPanel.Core;
using AdminPanel.Core.Exceptions;

namespace AdminPanel.WebApi
{
    /// <summary>
    /// Custom attribute for claim filtering
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _claimName;
        private readonly string _claimValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomAuthorizationAttribute"/> class.
        /// </summary>
        /// <param name="claimName">The name of the claim to check for.</param>
        /// <param name="claimValue">The value of the claim to check for.</param>
        public CustomAuthorizationAttribute(string claimName, string claimValue)
        {
            _claimName = claimName;
            _claimValue = claimValue;
        }

        /// <summary>
        /// Performs a check for the presence of the specified context.HttpContext.User = ClaimsPrincipal claim for the user.
        /// If the claim is absent, it sets the result to <see cref="ForbidResult"/>,
        /// preventing access to the action method or controller.
        /// </summary>
        /// <param name="context">The authorization filter context.</param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var existingClaimName = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == _claimName).Value;

            if (!Enum.TryParse(existingClaimName, out RoleType existingRoleClaim) || !Enum.TryParse(_claimValue, out RoleType necessaryRoleClaim))
                throw new ApplicationSystemException("Authorization role parsing error");

            if (existingRoleClaim > necessaryRoleClaim)
                context.Result = new ForbidResult();
        }
    }
}
