using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Assignment1.Startup))]
namespace Assignment1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
