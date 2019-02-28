using System.Collections.Generic;

namespace Core
{
    public class UpdateManager : IUpdate, ILateUpdate, IFixedUpdate
    {
        private static List<IUpdate> listUpdate = new List<IUpdate>();
        private static List<ILateUpdate> listLateUpdate = new List<ILateUpdate>();
        private static List<IFixedUpdate> listFixedUpdate = new List<IFixedUpdate>();

        public static void Add(object updateble)
        {
            if (updateble is IUpdate) listUpdate.Add(updateble as IUpdate);
            if (updateble is ILateUpdate) listLateUpdate.Add(updateble as ILateUpdate);
            if (updateble is IFixedUpdate) listFixedUpdate.Add(updateble as IFixedUpdate);
        }

        public static void Remove(object updateble)
        {
            if (updateble is IUpdate) listUpdate.Remove(updateble as IUpdate);
            if (updateble is ILateUpdate) listLateUpdate.Remove(updateble as ILateUpdate);
            if (updateble is IFixedUpdate) listFixedUpdate.Remove(updateble as IFixedUpdate);
        }

        public void OnUpdate()
        {
            for (var i = 0; i < listUpdate.Count; i++) listUpdate[i].OnUpdate();
        }

        public void FixedUpdate()
        {
            for (var i = 0; i < listFixedUpdate.Count; i++) listFixedUpdate[i].FixedUpdate();
        }

        public void LateUpdate()
        {
            for (var i = 0; i < listLateUpdate.Count; i++) listLateUpdate[i].LateUpdate();
        }
    }
}
