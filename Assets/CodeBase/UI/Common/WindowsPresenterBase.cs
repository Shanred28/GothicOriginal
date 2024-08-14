using UnityEngine;

namespace CodeBase.UI.Common
{
    public abstract class WindowsPresenterBase<TWindow> where TWindow : WindowsBase
    {
        public abstract void SetWindow(TWindow window);
    }
}

