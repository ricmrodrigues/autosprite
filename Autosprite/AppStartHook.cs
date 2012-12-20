using Microsoft.Web.Infrastructure.DynamicModuleHelper;

namespace AutoSprite
{
    public static class AppStartHook
    {
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(SpriteHttpModule));
        }
    }
}
