using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebBanNickGame.Startup))]
namespace WebBanNickGame
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
