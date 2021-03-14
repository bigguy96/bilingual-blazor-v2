using Microsoft.AspNetCore.Components;

namespace BilingualBlazor
{
    public class NavigationService
    {
        protected NavigationManager NavigationManager { get; }

        public NavigationService(NavigationManager navigationManager)
        {
            NavigationManager = navigationManager;
        }

        public void GoTo(string url)
        {
            NavigationManager.NavigateTo(url, true);
        }
    }
}

//https://stackoverflow.com/questions/61082293/can-i-call-navigationmanager-in-a-blazor-service