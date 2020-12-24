using System;

namespace BilingualBlazor.Utils
{
    public class StateContainer
    {
        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                NotifyStateChanged();
            }
        }

        public event Action OnChange;

        public void SetTitle(string value)
        {
            Title = value;
            NotifyStateChanged();
        }
        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}

//https://docs.microsoft.com/en-us/aspnet/core/blazor/state-management?view=aspnetcore-5.0&pivots=webassembly#in-memory-state-container-service-wasm