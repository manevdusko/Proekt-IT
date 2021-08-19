using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using Proekt_IT.Models;

[assembly: OwinStartupAttribute(typeof(Proekt_IT.Startup))]
namespace Proekt_IT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
