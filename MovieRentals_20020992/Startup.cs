using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MovieRentals_20020992.Startup))]
namespace MovieRentals_20020992
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
