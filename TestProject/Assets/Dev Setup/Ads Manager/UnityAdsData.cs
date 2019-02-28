namespace Dev.Ads
{
    [System.Serializable]
    public struct UnityAdsData
    {
        public string AppId_Android;
        public string AppId_IOS;

        public string InterstitialAdsIds;
        public string RewardAdsIds;

        public UnityAdsData(string appid_a,string appid_ios, string interstitialAdsIds, string rewardAdsIds)
        {
            AppId_IOS = appid_ios;
            AppId_Android = appid_a;
            InterstitialAdsIds = interstitialAdsIds;
            RewardAdsIds = rewardAdsIds;
        }
    }
}