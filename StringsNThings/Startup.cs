using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StringsNThings.Startup))]
namespace StringsNThings
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
