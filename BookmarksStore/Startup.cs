using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookmarksStore.Startup))]
namespace BookmarksStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
