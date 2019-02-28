using System;
using System.Collections.Generic;

namespace Managers
{
    public abstract class ManagerBase : IManager
    {

    }

    public abstract class ManagerBase<T> : IManager where T : class
    {
        protected Dictionary<Type, T> items = new Dictionary<Type, T>();

        protected abstract T2 Create<T2>() where T2 : T, new();

        protected virtual T2 Add<T2>() where T2 : T, new()
        {
            var _item = Create<T2>();
            items.Add(_item.GetType(), _item);
            return _item;
        }

        public virtual T2 Get<T2>() where T2 : T, new()
        {
            T _item = null;
            items.TryGetValue(typeof(T2), out _item);
            if (_item == null)
            {
                _item = Add<T2>();
            }
            return (T2)_item;
        }
    }
}
