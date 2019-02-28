using System;
using Managers;
using Tools;
using UnityEngine;
using Dev.Utilities;

namespace Games.MainGame
{
    public class MapContainer : ExtendedScriptableObject, IContainer<Map>
    {
        public Map[] Maps;
        
        public override void ParseData(object data)
        {
            MapData d = (MapData) data;
            Maps = d.Data;
        }

        public Map GetAdditionalItem(int index)
        {
            return new Map();
        }

        public Map GetItem(int index)
        {
            if (index >= Count)
            {
                Debug.LogError("[Map Container] Can't get sprite. Index out of range");
                throw new ArgumentOutOfRangeException();
            }
            return Maps[index];
        }

        public int Count => Maps.Length;
    }

    [Serializable]
    public struct Map
    {
        public byte[] Data;
        public MapType Type;
    }

    public enum MapType
    {
        Lose,Win
    }
}