using Microsoft.AspNetCore.Html;
using System;

namespace BilingualBlazor.State
{
    public class ApplicationState
    {
        public HtmlString AppTop  {get; set;}

        public void SetTop(HtmlString value)
        {
                AppTop = value;
                AppTopChanged?.Invoke();            
        }

        public event Action AppTopChanged;
    }
}