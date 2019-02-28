using System.IO;
using UnityEditor;
using UnityEngine;
using Dev.Utilities;
namespace Utilities
{
    public static class ScriptableObjectGenerator
    {
        public static void CreateAsset<T>(object data) where T : ExtendedScriptableObject
        {
            T asset = ScriptableObject.CreateInstance<T>();
            asset.ParseData(data);
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (path == "")
            {
                path = "Assets";
            }
            else if(Path.GetExtension(path) != "")
            {
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
            }
            string assetPathName = AssetDatabase.GenerateUniqueAssetPath(path + "/" + typeof(T) + ".asset");
            AssetDatabase.CreateAsset(asset,assetPathName);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }
    }
}