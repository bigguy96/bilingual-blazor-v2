using System;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Reflection;

namespace BilingualBlazor.Utils
{
    public static class RouteHelper
    {
        public static string GetAlternatePath(string path, string twoLetterRequestCulture)
        {
            if (path.Equals("/")) return path;

            var alternate = twoLetterRequestCulture.Equals("en", StringComparison.CurrentCultureIgnoreCase) ? "fr" : "en";
            var splitPath = path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            if (splitPath.Length < 2) return $"/{alternate}";

            var current = $"/{splitPath[0]}/{splitPath[1]}";

            // Get all the components whose base class is ComponentBase
            // Option 1
            var components = Assembly.GetExecutingAssembly().ExportedTypes.Where(t => t.IsSubclassOf(typeof(ComponentBase))).ToList();
            var routables = components.Where(c => c.GetCustomAttributes(inherit: true).OfType<RouteAttribute>().ToArray().Any());
            var alternateRoute = routables.Select(s => new { RouteAttributes = s.GetCustomAttributes(inherit: true).OfType<RouteAttribute>() })
                .SingleOrDefault(x => x.RouteAttributes.Any(a => a.Template.Contains(current)))?.RouteAttributes
                .SingleOrDefault(s => s.Template.Contains(alternate))?.Template;

            if (alternateRoute == null) return $"/{alternate}";

            var alternateRouteSplit = alternateRoute.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var alternatePath = $"/{alternateRouteSplit[0]}/{alternateRouteSplit[1]}";
            for (var i = 2; i < splitPath.Length; i++)
            {
                alternatePath += $"/{splitPath[i]}"; ;
            }

            return alternatePath;
        }
    }
}