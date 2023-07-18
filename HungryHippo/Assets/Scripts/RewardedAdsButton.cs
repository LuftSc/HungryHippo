using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener,IUnityAdsShowListener
{
    /*
    [SerializeField] private string _androidAdUnityId = "Interstitial_Android";
    [SerializeField] private string _iosAdUnityId = "Interstitial_iOS";
    private string _adUnityId;
    public static RewardedAdsButton S;

    private void Awake()
    {
        S = this;
        _adUnityId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iosAdUnityId
            : _androidAdUnityId;
    }

    private void Start()
    {
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
    
    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        // Проверяем, что реклама просмотрена. В какой сцене просмотрена реклама,
        // В зависимости от этого выдаётся разное вознаграждение
        if (placementId.Equals(_adUnityId) && showCompletionState.Equals(UnityAdsCompletionState.COMPLETED) &&
            SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            Debug.Log("0");
        }

        if (placementId.Equals(_adUnityId) && showCompletionState.Equals(UnityAdsCompletionState.COMPLETED))
        {
            Debug.Log("All Right");
            // Типо даётся жизнь за просмотр рекламы
            Time.timeScale = 1f;
        }
        LoadAd();
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
        
    } */
    
    [SerializeField] string androidAdID = "Rewarded_Android";
    [SerializeField] string iOSAdID = "Rewarded_iOS";
    [Header("Ссылка на кнопку, которая предлагает\nпродолжить игру после просмотра рекламы")]
    [SerializeField] private Button ContinueAfterAds;
    [SerializeField] private TextMeshProUGUI debug_text;
    private string adID;

    private void Awake()
    {
        adID = (Application.platform == RuntimePlatform.IPhonePlayer) ? iOSAdID : androidAdID;
        LoadAd();
        //ContinueAfterAds.interactable = false;
    }

    private void Start()
    {
        //Advertisement.Load(adID, this);
    }

    public void LoadAd()
    {
        Advertisement.Load(adID, this);
    }
    public void ShowAd()
    {
        
        //Advertisement.Load(adID, this);
        Advertisement.Show(adID, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        //ContinueAfterAds.interactable = true;
        debug_text.text = "Реклама загружена: " + placementId;
        Debug.Log("Реклама загружена: " + placementId);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        debug_text.text = $"Ошибка загрузки рекламы: {error.ToString()} - {message}";
        Debug.Log($"Ошибка загрузки рекламы: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        debug_text.text = $"Ошибка показа рекламы: {error.ToString()} - {message}";
        Debug.Log($"Ошибка показа рекламы: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        debug_text.text = "Старт показа реклама: " + placementId;
        Debug.Log("Старт показа реклама: " + placementId);
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        debug_text.text = "клик по рекламе: " + placementId;
        Debug.Log("клик по рекламе: " + placementId);
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            // тут код для добавления бонусов игроку.
            debug_text.text = "Юнити завершил показ рекламы, и добавил бонусы игроку.";
            Debug.Log("Юнити завершил показ рекламы, и добавил бонусы игроку.");
            Time.timeScale = 1f;
        }
    }
    
}
