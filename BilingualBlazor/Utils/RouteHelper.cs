using System;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Reflection;

namespace BilingualBlazor.Utils
{
    public static class RouteHelper
    {
        public static void GetAlternate(string path, string twoLetterRequestCulture)
        {
            if (path.Equals("/"))
            {
                return;
            }
            
            var alternate = twoLetterRequestCulture.Equals("en", StringComparison.CurrentCultureIgnoreCase)
                ? "fr"
                : "en";

            // Get all the components whose base class is ComponentBase
            // Option 1
            var components = Assembly.GetExecutingAssembly().ExportedTypes.Where(t => t.IsSubclassOf(typeof(ComponentBase))).ToList();
            var routables = components.Where(c => c.GetCustomAttributes(inherit: true).OfType<RouteAttribute>().ToArray().Any());
            var t = routables.Select(s=> new { RouteAttributes = s.GetCustomAttributes(inherit: true).OfType<RouteAttribute>() })
                .SingleOrDefault(x => x.RouteAttributes.Any(a => a.Template.Contains(path)))?.RouteAttributes
                .SingleOrDefault(s => s.Template.Contains(alternate))?.Template;

            // Option 2
            var routeAttributes = Assembly.GetExecutingAssembly().ExportedTypes
                .Where(t => t.IsSubclassOf(typeof(ComponentBase)))
                .Where(x => x.Name.Length > 1)
                .Select(s => new { RouteAttributes = s.GetCustomAttributes(inherit: true).OfType<RouteAttribute>() });

            //get route values for the current page and get the alternate language.
            var alternateRoute = routeAttributes
                .SingleOrDefault(x => x.RouteAttributes.Any(a => a.Template.Contains(path)))?.RouteAttributes
                .SingleOrDefault(s => s.Template.Contains(alternate))?.Template;
        }
    }
}