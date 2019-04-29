using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Smart_School.Startup))]
namespace Smart_School
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
