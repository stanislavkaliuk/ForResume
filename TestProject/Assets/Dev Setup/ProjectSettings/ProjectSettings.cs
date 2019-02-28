using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dev.Ads;
using Dev.Purchasing;
using UnityEditor;

namespace Dev
{
    #if UNITY_EDITOR
    [CreateAssetMenu(menuName = "ScriptableObjects/ProjectSettings", order = 3)]
    public class ProjectSettings : ScriptableObject
    {
        public ProjectData projectData;
        
        // Ads module data
        public bool enableAdMob = false;
        public bool enableUnityAds = false;
        public AdmobAdsData dataAdMob;
        public UnityAdsData dataUnityAds;
        
        //Translations
        public List<Object> ScenesForCheck;
        public TextAsset JsonFile;

        // IAP module data
        public bool enableIAP = false;
        public PurchaseData dataIAP;

        public OurAdsData ourAdsData;
    }

    public static class ProjectSettingsInEditor
    {
        [MenuItem("Dev/Edit Project Settings")]
        public static void OpenProjectSettings()
        {
            Selection.activeObject =
                AssetDatabase.LoadAssetAtPath<ProjectSettings>(
                    "Assets/Dev/ProjectSettings/ProjectSettings.asset");
        }
    }
    #endif
}
