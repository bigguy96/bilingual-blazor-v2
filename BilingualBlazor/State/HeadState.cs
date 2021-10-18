using System;

namespace BilingualBlazor.State
{
    public class HeadState
    {
        public string Title { get; private set; } = "";

        public void SetTitle(string title)
        {
            if (string.Equals(Title, title)) return;
            
            Title = title;
            HeadChanged?.Invoke();
        }

        public event Action HeadChanged;
    }
}