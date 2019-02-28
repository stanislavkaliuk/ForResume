using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public abstract class SingleMono<T> : SingletonPersistent<T> where T:SingleMono<T>
    {
        private Dictionary<Type, object> managers = new Dictionary<Type, object>();

        private static UpdateManager _updateManager;
        public static UpdateManager updateManager
        {
            get {
                if (_updateManager == null) _updateManager = new UpdateManager();
                return _updateManager;
            }
        }

        private static U Instantiate<U>() where U : new()
        {
            var _manager = new U();

            instance.managers.Add(_manager.GetType(), _manager);
            UpdateManager.Add(_manager);

            if (_manager is IAwake)
            {
                (_manager as IAwake).Awake();
            }

            return _manager;
        }
        
        public static U Get<U>() where U : new()
        {
            object _manager;
            instance.managers.TryGetValue(typeof(T), out _manager);
            if(_manager == null)
            {
                _manager = Instantiate<U>();
            }
            return (U)_manager;
        }

        public static bool Destroy<U>() where U : new()
        {
            object _manager;
            instance.managers.TryGetValue(typeof(U), out _manager);
            if (_manager != null)
            {
                if (_manager is IDestroy)
                {
                    instance.managers.Remove(_manager.GetType());
                    UpdateManager.Remove(_manager);

                    (_manager as IDestroy).Destroy();
                    return true;
                }
            }
            return false;
        }

        protected void Update()
        {
            updateManager.OnUpdate();
        }

        protected void FixedUpdate()
        {
            updateManager.FixedUpdate();
        }

        protected void LateUpdate()
        {
            updateManager.LateUpdate();
        }
    }
}
