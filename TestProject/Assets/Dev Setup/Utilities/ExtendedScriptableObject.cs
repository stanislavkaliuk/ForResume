using UnityEngine;

namespace Dev.Utilities
{
    public abstract class ExtendedScriptableObject : ScriptableObject
    {
        public abstract void ParseData(object data);
    }
}