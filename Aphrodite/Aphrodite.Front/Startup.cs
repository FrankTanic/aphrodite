using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Aphrodite.Front.Startup))]
namespace Aphrodite.Front
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
