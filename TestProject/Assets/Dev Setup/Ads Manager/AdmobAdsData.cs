namespace Dev.Ads
{
    [System.Serializable]
    public struct AdmobAdsData
    {
        public string AppId_Android;
        public string AppId_IOS;

        public string InterstitialAdsIds_Android;
        public string InterstitialAdsIds_IOS;

        public string RewardAdsIds_Android;
        public string RewardAdsIds_IOS;



        public AdmobAdsData(string appid_a, string appid_ios, 
            string interstitialAdsIds_a, string interstitialAdsIds_i, string rewardAdsIds_a, string rewardAdsIds_i)
        {
            AppId_IOS = appid_ios;
            AppId_Android = appid_a;
            InterstitialAdsIds_Android = interstitialAdsIds_a;
            InterstitialAdsIds_IOS = interstitialAdsIds_i;
            RewardAdsIds_Android = rewardAdsIds_a;
            RewardAdsIds_IOS = rewardAdsIds_i;
            
        }

    }
}