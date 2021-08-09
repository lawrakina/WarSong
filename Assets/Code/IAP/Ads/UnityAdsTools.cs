using System;
using Code.Extension;
using UnityEngine;
using UnityEngine.Advertisements;


namespace Code.IAP.Ads
{
    public class UnityAdsTools : MonoBehaviour, IAdsShower, IUnityAdsListener
    {
#if UNITY_IOS
        private string _gameId = "4129217";
#endif
#if UNITY_ANDROID
        private string _gameId = "4129216";
#endif
        private string _rewardPlace = "rewardedVideo";
        private string _interstitialPlace = "Interstitial_Android";


        private Action _callbackSuccessShowVideo;
      
        private void Start()
        {
            Advertisement.Initialize (_gameId, false);
        }
      
        public void ShowInterstitial()
        {
            _callbackSuccessShowVideo = null;
            Advertisement.Show(_interstitialPlace);
        }

        public void ShowVideo(Action successShow)
        {
            _callbackSuccessShowVideo = successShow;
            Advertisement.Show(_rewardPlace);
        }

        public void OnUnityAdsReady(string placementId)
        {
          Dbg.Log($"OnUnityAdsReady.placementId:{placementId}");
        }

        public void OnUnityAdsDidError(string message)
        {
            Dbg.Log($"OnUnityAdsDidError.placementId:{message}");
        }

        public void OnUnityAdsDidStart(string placementId)
        {
            Dbg.Log($"OnUnityAdsDidStart.placementId:{placementId}");
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            if (showResult == ShowResult.Finished)
                _callbackSuccessShowVideo?.Invoke();
        }
    }
}