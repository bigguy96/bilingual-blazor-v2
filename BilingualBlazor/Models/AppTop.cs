using System;
using System.Collections.Generic;
using System.Threading;

namespace BilingualBlazor.Models
{
    public class AppTop
    {
        public List<AppName> appName { get; set; }
        public List<Lnglink> lngLinks { get; set; }
        public List<Breadcrumb> breadcrumbs { get; set; }
    }

    public class AppName
    {
        private readonly string _twoLetterCulture = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
        public string text { get; set; }
        public string href { get { return _twoLetterCulture.Equals("en", StringComparison.OrdinalIgnoreCase) ? "/en/" : "/fr/"; } }
    }

    public class Lnglink
    {
        private readonly string _twoLetterCulture = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
        public string lang { get { return _twoLetterCulture.Equals("en", StringComparison.OrdinalIgnoreCase) ? "fr" : "en"; } }
        public string href { get; set; }
        public string text { get { return _twoLetterCulture.Equals("en", StringComparison.OrdinalIgnoreCase) ? "Français" : "English"; } }
    }

    public class Breadcrumb
    {
        public string title { get; set; }
        public string href { get; set; }
    }

}