using System;
using UnityEngine;
using Managers;
using Core;

namespace UI
{
    public class UIManager: ManagerBase, IAwake, IUpdate
    {
        private Vector2 resolution;
        private ScreenOrientation orientation;
        private ScreensManager _screens;
        public ScreensManager Screens
        {
            get
            {
                if (_screens == null) _screens = GameManager.Get<ScreensManager>();
                return _screens;
            }
        }

        private WindowsManager _windows;
        public WindowsManager Windows
        {
            get
            {
                if (_windows == null) _windows = GameManager.Get<WindowsManager>();
                return _windows;
            }
        }

        public static Action<Vector2> OnScreenResize;
        public static Action<ScreenOrientation> OnScreenOrientation;

        public void Awake()
        {
            resolution = new Vector2(Screen.width, Screen.height);
            orientation = Screen.orientation;
        }

        public void OnUpdate()
        {
            if (resolution.x != Screen.width || resolution.y != Screen.height)
            {
                resolution.x = Screen.width;
                resolution.y = Screen.height;
                var _orientation = resolution.x > resolution.y ? ScreenOrientation.Landscape : ScreenOrientation.Portrait;
                if (orientation != _orientation)
                {
                    orientation = _orientation;

                    OnScreenOrientation?.Invoke(orientation);
                }
                OnScreenResize?.Invoke(resolution);

            }
            
        }
    }
}