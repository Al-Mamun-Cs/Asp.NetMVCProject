using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AuthATMProject.Startup))]
namespace AuthATMProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
