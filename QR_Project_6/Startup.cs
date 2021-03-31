using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QR_Project_6.Startup))]
namespace QR_Project_6
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }
    }
}
