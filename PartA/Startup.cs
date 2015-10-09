using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PartA.Startup))]
namespace PartA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
