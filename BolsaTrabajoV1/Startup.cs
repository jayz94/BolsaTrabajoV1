using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BolsaTrabajoV1.Startup))]
namespace BolsaTrabajoV1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
