using Dev.Utilities;
namespace Dev.Ads
{
    public class AdmobAdsIds : ExtendedScriptableObject
    {
#if UNITY_ANDROID || UNITY_IOS
        public string AppId
        {
            get
            {
#if UNITY_ANDROID
                return _appId_Android;
#elif UNITY_IOS
                return _appId_IOS;
#endif
            }
        }

        public string InsterstitialId
        {
            get
            {
#if UNITY_ANDROID
                return _interstitial_android;
#elif UNITY_IOS
                return _interstitial_ios;
#endif
            }
        }

        public string RewardId
        {
            get
            {
#if UNITY_ANDROID
                return _reward_android;
#elif UNITY_IOS
                return _reward_ios;
#endif
            }
        }
        
        private string _appId_Android;
        private string _appId_IOS;

        private string _interstitial_android;
        private string _interstitial_ios;

        private string _reward_android;
        private string _reward_ios;

#endif
        public override void ParseData(object data)
        {
            AdmobAdsData adsData = (AdmobAdsData)data;
#if UNITY_ANDROID || UNITY_IOS
            _appId_Android = adsData.AppId_Android;
            _appId_IOS = adsData.AppId_IOS;
            _interstitial_android = adsData.InterstitialAdsIds_Android;
            _interstitial_ios = adsData.InterstitialAdsIds_IOS;
            _reward_android = adsData.RewardAdsIds_Android;
            _reward_ios = adsData.RewardAdsIds_IOS;
#endif
        }

    }
}
