using System;
using UnityEngine;
using Managers;

namespace UI
{
    public class WindowsManager : ManagerBase<IWindow>
    {
        public IWindow PreviousWindow { get; private set; }
        public IWindow CurrentWindow { get; private set; }

        protected override T Create<T>()
        {
            T _window;
            foreach (var _object in GameObject.FindGameObjectsWithTag(TagContainer.IWindowTag))
            {
                _window = _object.GetComponent<T>();
                if (_window != null)
                    return _window;
            }

            _window = new T();
            return _window;
        }

        public void Open<T>() where T : IWindow, new()
        {
            IWindow _window = Get<T>();

            if (CurrentWindow == null)
            {
                CurrentWindow = _window;
                CurrentWindow.Open();
            }
            else if (CurrentWindow.GetID() != _window.GetID())
            {
                PreviousWindow = CurrentWindow;
                CurrentWindow = _window;

                PreviousWindow.Close();
                CurrentWindow.Open();
            }
        }


    }
}
