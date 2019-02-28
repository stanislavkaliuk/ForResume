using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;
#if UNITY_ANDROID || UNITY_IOS
using GoogleMobileAds.Api;
using UnityEngine.Advertisements;
#endif
namespace Dev.Ads
{
    /// <summary>
    /// Ads Manager. Subscribe on OnReward event and unsubscribe after script finish lifetime (onDestroy on your script)
    /// </summary>
    public class AdsManager : MonoBehaviour
    {
        public static AdsManager Instance;
        public event EventHandler OnRewarded; 
        public float PossibilityToShowUnityAds;
        public UnityAdsIds UnityAdsIDs;
        public AdmobAdsIds AdmobAdIDs;
        
#if UNITY_ANDROID || UNITY_IOS
        private InterstitialAd _admobInterstitial;
        private RewardBasedVideoAd _admobReward;
       

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                StartCoroutine(InitAds());
                DontDestroyOnLoad(gameObject);
            }
           
        }

        private IEnumerator InitAds()
        {
#if UNITY_ANDROID || UNITY_IOS
            Advertisement.Initialize(UnityAdsIDs.AppId);
            yield return new WaitForSeconds(0.5f);
            MobileAds.Initialize(AdmobAdIDs.AppId);
            RequestInterstitial();
            _admobReward = RewardBasedVideoAd.Instance;
            _admobReward.OnAdRewarded += OnAdRewarded;
            RequestReward();
#endif
        }

        private void OnAdRewarded(object sender, Reward e)
        {
            if (OnRewarded != null) 
                OnRewarded.Invoke(this, null);
            else
            {
                Debug.LogWarning("OnReward doesn't have subscriptions");
            }
        }

        private void RequestReward()
        {
            AdRequest request = new AdRequest.Builder().Build();
            _admobReward.LoadAd(request,AdmobAdIDs.RewardId);
        }


        private void RequestInterstitial()
        {
            string id = AdmobAdIDs.InsterstitialId;
            _admobInterstitial = new InterstitialAd(id);

            AdRequest request = new AdRequest.Builder().Build();
            _admobInterstitial.LoadAd(request);
        }

        public void ShowInterstitialAd()
        {
            if (Random.value > PossibilityToShowUnityAds)
            {
                if(Advertisement.IsReady(UnityAdsIDs.InterstititalId))
                    Advertisement.Show(UnityAdsIDs.InterstititalId);
            }
            else
            {
                if (_admobInterstitial.IsLoaded())
                {
                    _admobInterstitial.Show();
                }
            }
            
        }

        public void ShowRewardAd()
        {
            if (Random.value > PossibilityToShowUnityAds)
            {
                var options = new ShowOptions {resultCallback = OnReward};
                Advertisement.Show(UnityAdsIDs.RewardId, options);
            }
            else
            {
                if (_admobReward.IsLoaded())
                {
                    _admobReward.Show();
                }
            }
        }

        public void OnReward(ShowResult result)
        {
            switch(result)
            {
                case ShowResult.Finished:
                    if (OnRewarded != null) 
                        OnRewarded.Invoke(this, null);
                    else
                    {
                        Debug.LogWarning("OnReward doesn't have subscriptions");
                    }
                    break;
                case ShowResult.Skipped:
                    Debug.Log("Ad was skipped");
                    break;
                case ShowResult.Failed:
                    Debug.LogError("Fail to Show Rewarding video from Unity Ads");
                    break;
            }
        }
#endif
    }
}
