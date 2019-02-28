using Dev.Utilities;

namespace Dev.Ads
{
    public class UnityAdsIds : ExtendedScriptableObject
    {
#if UNITY_ANDROID || UNITY_IOS
        public string AppId
        {
            get
            {
#if UNITY_ANDROID
                return AppId_Android;
#elif UNITY_IOS
                return AppId_IOS;
#endif
            }
        }
#endif

        public string InterstititalId
        {
            get { return _interstititalId; }
        }

        public string RewardId
        {
            get { return _rewardId; }
        }

        private string _interstititalId;
        private string _rewardId;

        private string AppId_Android;
        private string AppId_IOS;



        public override void ParseData(object data)
        {
            UnityAdsData adsData = (UnityAdsData)data;
            AppId_Android = adsData.AppId_Android;
            AppId_IOS = adsData.AppId_IOS;
            _interstititalId = adsData.InterstitialAdsIds;
            _rewardId = adsData.RewardAdsIds;
        }
    }
}
