using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{

    [SerializeField] private string _androidAdUnityId = "Interstitial_Android";
    [SerializeField] private string _iosAdUnityId = "Interstitial_iOS";
    private string _adUnityId;
    public static InterstitialAd S;

    /*
    private void Awake()
    {
        S = this;
        _adUnityId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iosAdUnityId
            : _androidAdUnityId;
        LoadAd();
    }

    public void LoadAd()
    {
        Advertisement.Load(_adUnityId, this);
    }

    public void ShowAd()
    {
        Advertisement.Show(_adUnityId, this);
    } 
    */
    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        //ShowAd();
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        
    }

    
}
