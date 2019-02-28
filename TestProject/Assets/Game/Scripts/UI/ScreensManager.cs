using System;
using UnityEngine;
using System.Collections.Generic;
using Managers;
using Core;

namespace UI
{
    public class ScreensManager : ManagerBase<IScreen>
    {
        public IScreen PreviousScreen { get; private set; }
        public IScreen CurrentScreen { get; private set; }

        protected override T Create<T>()
        {
            T _screen;
            foreach (var _object in GameObject.FindGameObjectsWithTag(TagContainer.IScreenTag))
            {
                _screen = _object.GetComponent<T>();
                if (_screen != null)
                    return _screen;
            }

            _screen = new T();
            return _screen;
        }

        public void Set<T>() where T : IScreen, new()
        {
            IScreen screen = Get<T>();

            if (CurrentScreen == null)
            {
                CurrentScreen = screen;
                CurrentScreen.Activate();
            }
            else if (CurrentScreen.GetID() != screen.GetID())
            {
                PreviousScreen = CurrentScreen;
                CurrentScreen = screen;

                PreviousScreen.Deactivate();
                CurrentScreen.Activate();
            }
        }


    }
}
