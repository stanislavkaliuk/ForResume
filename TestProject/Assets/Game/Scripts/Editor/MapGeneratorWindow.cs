using Games.MainGame;
using UnityEditor;
using UnityEngine;

namespace Utilities
{
    public class MapGeneratorWindow : EditorWindow
    {
        [MenuItem("Cleopatra's Diary/Generators/MapGenerator")]
        public static void OpenMapGenerator()
        {
            GetWindow<MapGeneratorWindow>("Map Generator");
        }

        private MapData _mapData;
        private byte[,] currentMap;
        
        private int MapWidth = 3;
        private int MapHeight = 3;
        private int MapsAmount = 5;
        private int SymbolsAmount = 5;
        private bool _generated;
        private int _currentMapIndex;


        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            if (!_generated)
            {
                MapWidth = EditorGUILayout.IntField("Map Width", MapWidth);
                MapHeight = EditorGUILayout.IntField("Map Height", MapHeight);
                MapsAmount = EditorGUILayout.IntField("Maps Amount", MapsAmount);
                SymbolsAmount = EditorGUILayout.IntField("Symbols Amount", SymbolsAmount);

                if (GUILayout.Button("Create Empty Data"))
                {
                    _mapData = new MapData(new Map[MapsAmount]);
                    for (int i = 0; i < _mapData.Data.Length; i++)
                    {
                        _mapData.Data[i].Data = new byte[MapWidth*MapHeight];
                    }
                    _generated = true;
                }

                if (GUILayout.Button("Import"))
                {
                    //todo import data from scriptable object
                }
            }
            else
            {
                _currentMapIndex = EditorGUILayout.IntField("Current Index", _currentMapIndex);
                _currentMapIndex = Mathf.Clamp(_currentMapIndex, 0, MapsAmount-1);
                Map map = _mapData.Data[_currentMapIndex];
                RenderMap(map);
                EditorGUILayout.Space();
                map.Type = (MapType)EditorGUILayout.EnumPopup("Map Type", map.Type);
                _mapData.Data[_currentMapIndex] = map;
                if (GUILayout.Button("Save"))
                {
                    MapGenerator.CreateMapAsset(_mapData);
                    Close();
                }
            }
                    
            EditorGUILayout.EndVertical();
        }

        private void RenderMap(Map map)
        {
            int start = MapWidth * MapHeight - 1;
            int lineEnd = start;
            for (int i = start; i >= 0; i--)
            {
                if (i == lineEnd)
                {
                    lineEnd -= MapWidth;
                    EditorGUILayout.BeginHorizontal();
                }

                if (i % MapWidth == 0)
                {
                    for (int x = 0; x < MapWidth; x++)
                    {
                        if(GUILayout.Button(map.Data[i+x].ToString(),GUILayout.Height(25),
                            GUILayout.Width(75)))
                        {
                            UpdateData(ref map.Data[i+x]);
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
        }

        private void UpdateData(ref byte data)
        {
            data += 1;
            if (data == SymbolsAmount)
            {
                data = 0;
            }
        }
    }
}