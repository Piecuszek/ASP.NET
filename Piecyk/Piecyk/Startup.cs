using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Piecyk.Startup))]
namespace Piecyk
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
