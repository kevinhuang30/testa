using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CreditWorks.Startup))]
namespace CreditWorks
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
