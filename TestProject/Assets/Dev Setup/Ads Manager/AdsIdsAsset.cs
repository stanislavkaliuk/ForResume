using Dev.Utilities;
using UnityEditor;

namespace Dev.Ads
{
    public static class AdsIdsAsset
    {
        public static void CreateUnityAdsIdsAsset(UnityAdsData adsData)
        {
            ScriptableObjectUtility.CreateAsset<UnityAdsIds>(adsData);
        }

        public static void CreateAdmobIdsAsset(AdmobAdsData adsData)
        {
            ScriptableObjectUtility.CreateAsset<AdmobAdsIds>(adsData);
        }
    }
}