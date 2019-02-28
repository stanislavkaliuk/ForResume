using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Dev.Ads;
using Dev.Purchasing;
using Dev.OurAdsModule;
namespace Dev
{
    [CustomEditor(typeof(ProjectSettings))]
    public class ProjectSettingsEditor : ExtendedEditor
    {
        
        ReorderableList _listProducts;

        ReorderableList _listOurAds;
        ProjectSettings _settings;
        SerializedProperty _dataAdMob;
        SerializedProperty _dataUnityAds;
        SerializedProperty _dataIAP;
        SerializedProperty _projectData;

        SerializedProperty _ourAdData;
        
        
        private bool showProjectModule = false;
        private bool showAndroid = false;
        private bool showIOS = false;
        
        private bool showModuleAds = false;
        private bool showAdMob = false;
        private bool showUnityAds = false;
        private bool showModuleIAP = false;
        private bool showModuleAdvanced;
        
        private bool showOurAdModule = false;
        

        public void OnEnable()
        {
            _settings = target as ProjectSettings;

            //project data init
            _projectData = serializedObject.FindProperty("projectData");
            // Ads module init
            _dataAdMob = serializedObject.FindProperty("dataAdMob");
            _dataUnityAds = serializedObject.FindProperty("dataUnityAds");
            

            // IAP module init
            _dataIAP = serializedObject.FindProperty("dataIAP");
            _listProducts = new ReorderableList(serializedObject, _dataIAP.FindPropertyRelative("Products"), true, true, true, true);
            _listProducts.drawElementCallback = DrawProductsElement;
            _listProducts.drawHeaderCallback = DrawProductsHeader;

            _ourAdData = serializedObject.FindProperty("ourAdsData");
            _listOurAds = new ReorderableList(serializedObject, _ourAdData.FindPropertyRelative("Ads"),false,true,true,true);
            _listOurAds.drawHeaderCallback = DrawOurAdHeader;
            _listOurAds.drawElementCallback = DrawOurAdElement;
        }

        public override void OnInspectorGUI()
        {
            Header();


            //project data settings
            showProjectModule = GUILayout.Toggle(showProjectModule, "Project Data", styles.Header, GUILayout.Height(20), GUILayout.ExpandWidth(true));
            if(showProjectModule) ProjectDataSettings();
            
            // Ads module show
            showModuleAds = GUILayout.Toggle(showModuleAds, "ADs Setting", styles.Header, GUILayout.Height(20), GUILayout.ExpandWidth(true));
            if (showModuleAds) AdsSettings();

            // IAP module show
            showModuleIAP = GUILayout.Toggle(showModuleIAP, "IAP Setting", styles.Header, GUILayout.Height(20), GUILayout.ExpandWidth(true));
            if (showModuleIAP) IAPSettings();

//            showBiirdModule = GUILayout.Toggle(showBiirdModule, "Biird", styles.Header, GUILayout.Height(20),
//                GUILayout.ExpandWidth(true));
//            if(showBiirdModule) BiirdSettings();

            showOurAdModule = GUILayout.Toggle(showOurAdModule,"Our Ads", styles.Header,GUILayout.Height(20),
                GUILayout.ExpandWidth(true));
            if(showOurAdModule) OurAdSettings();
            
            showModuleAdvanced = GUILayout.Toggle(showModuleAdvanced,"Advanced Settings", styles.Header,GUILayout.Height(20),
                GUILayout.ExpandWidth(true));
            if(showModuleAdvanced) AdvancedSettings();

            serializedObject.ApplyModifiedProperties();
        }

        private void Header()
        {
            EditorGUILayout.BeginVertical();
            GUILayout.Label("PROJECT SETTINGS", styles.TitleText);
            GUILayout.Label("© Stanislav Kaliuk", EditorStyles.centeredGreyMiniLabel);
            EditorGUILayout.EndVertical();
        }
        // ----------------------------------------------------------------------------------------------------
        //          ADS MODULE SETTING  
        // ----------------------------------------------------------------------------------------------------
        private void AdsSettings()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(8);
            EditorGUILayout.BeginVertical();

            // Ad Mob Settings
            showAdMob = GUILayout.Toggle(showAdMob, "AdMob", styles.Header, GUILayout.Height(20), GUILayout.ExpandWidth(true));
            if (showAdMob)
            {
                    _settings.enableAdMob = true;
                    EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                    GUILayout.Label("AdMob - iOS", styles.BoldText);
                    EditorGUILayout.PropertyField(_dataAdMob.FindPropertyRelative("AppId_IOS"), new GUIContent("App ID:"));
                    EditorGUILayout.PropertyField(_dataAdMob.FindPropertyRelative("InterstitialAdsIds_IOS"), new GUIContent("Interstitial Ads ID"));
                    EditorGUILayout.PropertyField(_dataAdMob.FindPropertyRelative("RewardAdsIds_IOS"), new GUIContent("Reward Ads ID"));
                    GUILayout.Label("AdMob - Android", styles.BoldText);
                    EditorGUILayout.PropertyField(_dataAdMob.FindPropertyRelative("AppId_Android"), new GUIContent("App ID:"));
                    EditorGUILayout.PropertyField(_dataAdMob.FindPropertyRelative("InterstitialAdsIds_Android"), new GUIContent("Interstitial Ads ID"));
                    EditorGUILayout.PropertyField(_dataAdMob.FindPropertyRelative("RewardAdsIds_Android"), new GUIContent("Reward Ads ID"));
                    EditorGUILayout.EndVertical();
                    if (GUILayout.Button("Generate Admob Ads Ids"))
                    {
                        AdmobAdsData data = _settings.dataAdMob;
                        AdsIdsAsset.CreateAdmobIdsAsset(data);
                    }
            }

            // Unity Ads Settings
            showUnityAds = GUILayout.Toggle(showUnityAds, "Unity Ads", styles.Header, GUILayout.Height(20), GUILayout.ExpandWidth(true));
            if (showUnityAds)
            {
                    EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                    EditorGUILayout.PropertyField(_dataUnityAds.FindPropertyRelative("AppId_IOS"), new GUIContent("App ID (iOS):"));
                    EditorGUILayout.PropertyField(_dataUnityAds.FindPropertyRelative("AppId_Android"), new GUIContent("App ID (Android):"));
                    EditorGUILayout.PropertyField(_dataUnityAds.FindPropertyRelative("InterstitialAdsIds"), new GUIContent("Interstitial Ads ID"));
                    ArrayGUI(_dataUnityAds.FindPropertyRelative("RewardAdsIds"),"Reward Ads ID");
                    EditorGUILayout.EndVertical();
                    if (GUILayout.Button("Generate Unity Ads Ids"))
                    {
                        UnityAdsData data = _settings.dataUnityAds;
                        AdsIdsAsset.CreateUnityAdsIdsAsset(data);
                    }
            }

            EditorGUILayout.EndVertical();
            GUILayout.Space(8);
            EditorGUILayout.EndHorizontal();
        }

        private void ArrayGUI(SerializedProperty property, string label)
        {
            SerializedProperty arraySizeProp = property.FindPropertyRelative("Array.size");
            EditorGUILayout.PropertyField(arraySizeProp, new GUIContent(label + " size"));
 
            EditorGUI.indentLevel ++;
 
            for (int i = 0; i < arraySizeProp.intValue; i++) {
                EditorGUILayout.PropertyField(property.GetArrayElementAtIndex(i),new GUIContent("Reward Placement "+ (i+1)));
            }
 
            EditorGUI.indentLevel --;
        }
        // ----------------------------------------------------------------------------------------------------
        //          IAP MODULE SETTING  
        // ----------------------------------------------------------------------------------------------------
        private void IAPSettings()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(8);
            EditorGUILayout.BeginVertical();
            _settings.enableIAP = true;
            _listProducts.DoLayoutList();
            if (GUILayout.Button("Generate Purchase Ids"))
            {
                PurchaseData data = _settings.dataIAP;
                PurchaseIdsGenerator.GeneratePurchaseIds(data);
            }
            EditorGUILayout.EndVertical();
            GUILayout.Space(8);
            EditorGUILayout.EndHorizontal();
        }
        
        /*
         *     PROJECT DATA SETTINGS
         */
        private void ProjectDataSettings()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(8);
            EditorGUILayout.BeginVertical();
            EditorGUILayout.PropertyField(_projectData.FindPropertyRelative("ProductName"), new GUIContent("Product Name:"));
            EditorGUILayout.PropertyField(_projectData.FindPropertyRelative("VersionTemplate"), new GUIContent("Version Template: (ex. x.xx.xxx or x.xx)"));
            EditorGUILayout.PropertyField(_projectData.FindPropertyRelative("ScriptingRuntimeVersion"), new GUIContent("Scripting Runtime Version:"));
            EditorGUILayout.PropertyField(_projectData.FindPropertyRelative("BackendVersion"), new GUIContent("Backend Type:"));
            
            showAndroid = GUILayout.Toggle(showAndroid, "Android", styles.Header, GUILayout.Height(20), GUILayout.ExpandWidth(true));
            if (showAndroid)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUILayout.PropertyField(_projectData.FindPropertyRelative("AndroidPackageName"), new GUIContent("Android Package Name (skip com.dev.): "));
                EditorGUILayout.PropertyField(_projectData.FindPropertyRelative("KeyStorePassword"), new GUIContent("Keystore Password:"));
                EditorGUILayout.PropertyField(_projectData.FindPropertyRelative("KeyAliasPassword"), new GUIContent("Key Alias Password:"));
                EditorGUILayout.PropertyField(_projectData.FindPropertyRelative("AndroidArchitecture"),
                    new GUIContent("Android Architecture:"));
                EditorGUILayout.EndVertical();
            }
            
            showIOS = GUILayout.Toggle(showIOS, "iOS", styles.Header, GUILayout.Height(20), GUILayout.ExpandWidth(true));
            if (showIOS)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUILayout.PropertyField(_projectData.FindPropertyRelative("IOSPackageName"), new GUIContent("iOS Package Name (skip com.dev.): "));
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }

        private void DrawProductsElement(Rect rect, int i, bool isActive, bool isFocused)
        {
            var element = _dataIAP.FindPropertyRelative("Products").GetArrayElementAtIndex(i);
            rect.y += 2;
            EditorGUI.LabelField(new Rect(rect.x, rect.y, 20, EditorGUIUtility.singleLineHeight), (i + 1).ToString());
#if UNITY_PURCHASING
            
            EditorGUI.PropertyField(new Rect(rect.x + 21, rect.y, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Editorid"), GUIContent.none);
            EditorGUI.PropertyField(new Rect(rect.x + 23 + 100, rect.y, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("AndroidStoreId"), GUIContent.none);
            EditorGUI.PropertyField(new Rect(rect.x + 25 + 200, rect.y, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("iOSStoreId"), GUIContent.none);
            EditorGUI.PropertyField(new Rect(rect.x + 27 + 300, rect.y, 125, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Type"), GUIContent.none);

#else
            EditorGUI.PropertyField(new Rect(rect.x + 41, rect.y, Screen.width - 150, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Editorid"), GUIContent.none);
            EditorGUI.PropertyField(new Rect(rect.x + 43 + Screen.width - 150, rect.y, 78, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("AndroidStoreId"), GUIContent.none);
            EditorGUI.PropertyField(new Rect(rect.x + 45 + Screen.width - 300, rect.y, 78, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("iOSStoreId"), GUIContent.none);
#endif

        }

        private void DrawOurAdElement(Rect rect, int i, bool isActive, bool isFocused)
        {
            var element = _ourAdData.FindPropertyRelative("Ads").GetArrayElementAtIndex(i);
            rect.y+=2;
            EditorGUI.LabelField(new Rect(rect.x, rect.y, 20, EditorGUIUtility.singleLineHeight), (i + 1).ToString());
            EditorGUI.PropertyField(new Rect(rect.x + 21, rect.y, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("AndroidLink"), GUIContent.none);
            EditorGUI.PropertyField(new Rect(rect.x + 23 + 100, rect.y, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("iOSLink"), GUIContent.none);
            EditorGUI.PropertyField(new Rect(rect.x + 25 + 200, rect.y, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("AndroidAdImage"), GUIContent.none);
            EditorGUI.PropertyField(new Rect(rect.x + 27 + 300, rect.y, 125, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("iOSAdImage"), GUIContent.none);
        }

        private void DrawProductsHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, "IAP Products: Editor(base)|Android|iOS|Product Type");
        }

        private void DrawOurAdHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, "Our Ads: Link Android|Link iOS|Android Image|iOS Image");
        }


        //----------------------------------------------------------------------------------
        //      OUR ADS MODULE SETTING
        //----------------------------------------------------------------------------------

        private void OurAdSettings()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(8);
            EditorGUILayout.BeginVertical();
            _listOurAds.DoLayoutList();
            if(GUILayout.Button("Generate Our Ads Data"))
            {
                OurAdsData data = _settings.ourAdsData;
                OurAdsGenerator.GenerateOurAds(data);
            }
            EditorGUILayout.EndVertical();
            GUILayout.Space(8);
            EditorGUILayout.EndHorizontal();
        }
        
        //----------------------------------------------------------------------------------
        //      ADVANCED SETTINGS
        //----------------------------------------------------------------------------------
        private void AdvancedSettings()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("MultiDex Support");
            //path Assets/Dev Setup/ProjectSettings/Editor/Resources/
            if (GUILayout.Button("Enable MultiDex Support"))
            {
                if (!IsGradleTemplateFileExists())
                {
                    EditorUtility.DisplayDialog("Warning!",
                        "Ensure that Custom Gradle File checkbox enabled. You can find it in Player Settings -> Android -> Publish Settings -> Custom Gradle File",
                        "I'll do it Master");
                    return;
                }
                //Assets/Dev Setup/ProjectSettings/Editor/Resources/mainTemplate.txt
                //Gradle File
                string gradleOriginalPath = Path.Combine(Application.dataPath, "Plugins/Android/mainTemplate.gradle");
                string gradleSourcePath = Path.Combine(Application.dataPath,
                    "Dev Setup/ProjectSettings/Editor/Resources/mainTemplate.txt");
                string text = File.ReadAllText(gradleSourcePath);
                File.WriteAllText(gradleOriginalPath,string.Empty);
                File.WriteAllText(gradleOriginalPath, text);
                
                //Manifest File
                
                string manifestFileSource = Path.Combine(Application.dataPath,
                    "Dev Setup/ProjectSettings/Editor/Resources/template-AndroidManifest.txt");

                string manifest = File.ReadAllText(manifestFileSource);
                manifest = manifest.Replace("com.dev.gamename", PlayerSettings.applicationIdentifier);
                string devPluginDirectory = Path.Combine(Application.dataPath, "Plugins/Android/Dev");
                if (!Directory.Exists(devPluginDirectory))
                {
                    Directory.CreateDirectory(devPluginDirectory);
                }

                if (File.Exists(Path.Combine(devPluginDirectory, "AndroidManifest.xml")))
                {
                    File.WriteAllText(Path.Combine(devPluginDirectory, "AndroidManifest.xml"),manifest);
                }
                else
                {
                    using (StreamWriter wr = File.CreateText(Path.Combine(Application.dataPath,"Plugins/Android/Dev/AndroidManifest.xml")))
                    {
                        wr.Write(manifest);
                    }
                }
                
                
            }
            EditorGUILayout.EndVertical();
            //copy to Assets/Plugins/Android
        }
        
        

        private bool IsGradleTemplateFileExists()
        {
            string path = Path.Combine(Application.dataPath, "Plugins/Android/mainTemplate.gradle");
            return File.Exists(path);
        }
        
    }
}
