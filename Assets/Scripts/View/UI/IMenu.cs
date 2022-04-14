using System;

namespace UCARPG.View.UI
{
    public interface IMenu
    {
        public event Action Opened;
        public event Action Closed;
    }
}