using Dev.Ads;
using UnityEditor;
using UnityEngine;

namespace KAUGamesLviv
{
    public class SetupAdsEditor : EditorWindow
    {

        private string _unity_ads_id_android;
        private string _unity_ads_id_ios;

        private string _admob_ads_id_android;
        private string _admob_ads_id_ios;

        private string _unity_ads_interstitial;
        private string _unity_ads_reward;

        private string _admob_ads_interstitial_android;
        private string _admob_ads_interstitial_ios;

        private string _admob_ads_reward_android;
        private string _admob_ads_reward_ios;

        
        [MenuItem("Dev/Ads Manager Window")]
        public static void ShowWindow()
        {
            GetWindow<SetupAdsEditor>("Ads Setup");
        }
        
        
        private void OnGUI()
        {
            EditorGUILayout.LabelField("Unity Ads",EditorStyles.boldLabel);
            _unity_ads_id_android = EditorGUILayout.TextField("Android App Id", _unity_ads_id_android);
            _unity_ads_id_ios = EditorGUILayout.TextField("iOS App Id", _unity_ads_id_ios);

            _unity_ads_interstitial = EditorGUILayout.TextField("Interstitial Id", _unity_ads_interstitial);
            _unity_ads_reward = EditorGUILayout.TextField("Reward Ad Id", _unity_ads_reward);

            if (GUILayout.Button("Generate Unity Ads Ids"))
            {
                UnityAdsData data = new UnityAdsData(_unity_ads_id_android,_unity_ads_id_ios,_unity_ads_interstitial,_unity_ads_reward);
                AdsIdsAsset.CreateUnityAdsIdsAsset(data);
            }
            
            EditorGUILayout.LabelField("Admob Ads", EditorStyles.boldLabel);
            _admob_ads_id_android = EditorGUILayout.TextField("Android App Id", _admob_ads_id_android);
            _admob_ads_id_ios = EditorGUILayout.TextField("iOS App Id", _admob_ads_id_ios);

            _admob_ads_interstitial_android =
                EditorGUILayout.TextField("Android Interstitial Id", _admob_ads_interstitial_android);
            _admob_ads_interstitial_ios =
                EditorGUILayout.TextField("iOS Interstitial Id", _admob_ads_interstitial_ios);

            _admob_ads_reward_android = EditorGUILayout.TextField("Android Reward Id", _admob_ads_reward_android);
            _admob_ads_reward_ios = EditorGUILayout.TextField("iOS Reward Id", _admob_ads_reward_ios);

            if (GUILayout.Button("Generate Admob Ads Ids"))
            {
                AdmobAdsData data = new AdmobAdsData(_admob_ads_id_android,_admob_ads_id_ios,_admob_ads_interstitial_android,_admob_ads_interstitial_ios,_admob_ads_reward_android,_admob_ads_reward_ios);
                AdsIdsAsset.CreateAdmobIdsAsset(data);
            }

        }
    }
}