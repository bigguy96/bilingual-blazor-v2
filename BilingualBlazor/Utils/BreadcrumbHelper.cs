using BilingualBlazor.Resources.Shared;
using System.Resources;

namespace BilingualBlazor.Utils
{
    public class BreadcrumbHelper
    {
        public string Test()
        {
            var rm = new ResourceManager(typeof(Common));
            return rm.GetString("Home");
        }
    }
}