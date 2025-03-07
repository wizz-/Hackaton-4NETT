using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Hackaton.Web.Auth.Blazor
{
    public class BaseAuthComponent : ComponentBase
    {
        [Inject] protected AuthenticationStateProvider AuthStateProvider { get; set; } = default!;
        protected ClaimsPrincipal User { get; private set; } = new ClaimsPrincipal(new ClaimsIdentity());
        protected bool IsAuthenticated => User.Identity?.IsAuthenticated ?? false;
        protected string UserName => User.Identity?.Name ?? "Visitante";

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            User = authState.User;
        }
    }
}
