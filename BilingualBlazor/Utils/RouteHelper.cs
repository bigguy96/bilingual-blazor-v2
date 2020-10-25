using System;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Reflection;

namespace BilingualBlazor.Utils
{
    public static class RouteHelper
    {
        public static string GetAlternate(string path, string twoLetterRequestCulture)
        {
            if (path.Equals("/")) return path;

            var alternate = twoLetterRequestCulture.Equals("en", StringComparison.CurrentCultureIgnoreCase) ? "fr" : "en";
            var split = path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            if (split.Length == 1)
            {
                return $"/{alternate}";
            }

            if (split.Length >= 2)
            {
                path = $"{split[0]}/{split[1]}";
            }

            // Get all the components whose base class is ComponentBase
            // Option 1
            var components = Assembly.GetExecutingAssembly().ExportedTypes.Where(t => t.IsSubclassOf(typeof(ComponentBase))).ToList();
            var routables = components.Where(c => c.GetCustomAttributes(inherit: true).OfType<RouteAttribute>().ToArray().Any());
            var alternateRoute = routables.Select(s => new { RouteAttributes = s.GetCustomAttributes(inherit: true).OfType<RouteAttribute>() })
                .SingleOrDefault(x => x.RouteAttributes.Any(a => a.Template.Contains(path)))?.RouteAttributes
                .SingleOrDefault(s => s.Template.Contains(alternate))?.Template;

            if (alternateRoute == null) return $"/{alternate}";

            var alternateRouteSplit = alternateRoute.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var newRoute = $"/{alternateRouteSplit[0]}/{alternateRouteSplit[1]}";
            for (var i = 2; i < split.Length; i++)
            {
                newRoute += $"/{split[i]}"; ;
            }

            return newRoute;
        }
    }
}