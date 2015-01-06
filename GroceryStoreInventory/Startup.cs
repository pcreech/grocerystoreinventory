using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GroceryStoreInventory.Startup))]
namespace GroceryStoreInventory
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
