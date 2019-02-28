using System;

namespace Games.MainGame
{
    [Serializable]
    public struct MapData
    {
        public Map[] Data;
        
        public MapData(Map[] data)
        {
            Data = data;
        }
    }
}