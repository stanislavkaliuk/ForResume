using Games.MainGame;

namespace Utilities
{
    public static class MapGenerator
    {
        public static void CreateMapAsset(MapData mapData)
        {
            ScriptableObjectGenerator.CreateAsset<MapContainer>(mapData);
        }
    }
}