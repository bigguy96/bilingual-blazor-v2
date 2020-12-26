using System;

namespace BilingualBlazor.State
{
    public class HeadState
    {
        public string Title => _title;

        private string _title = "";

        public void SetTitle(string title)
        {
            if (!string.Equals(_title, title))
            {
                _title = title;
                HeadChanged?.Invoke();
            }
        }

        public event Action HeadChanged;
    }
}