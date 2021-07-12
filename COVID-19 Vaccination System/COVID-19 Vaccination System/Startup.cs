using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(COVID_19_Vaccination_System.Startup))]
namespace COVID_19_Vaccination_System
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
