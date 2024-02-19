using Tekton.API.Infrastructure;
using Tekton.Infrastructure.Identity;

namespace Tekton.Web.Endpoints;
public class Users : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapIdentityApi<ApplicationUser>();
    }
}
