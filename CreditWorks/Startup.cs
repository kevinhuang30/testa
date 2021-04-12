using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SecuredSigning.Startup))]
namespace SecuredSigning
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

        }
    }
}
