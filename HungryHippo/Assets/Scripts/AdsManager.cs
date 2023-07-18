using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private bool testMod;
    [SerializeField] private string androidGameId;
    [SerializeField] private string iosGameId;
    [SerializeField] private TextMeshProUGUI debug_text;
    private string gameId;
    private void Awake()
    {
        InitializedAds();
    }
    public void InitializedAds()
    {
        gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? iosGameId
            : androidGameId;
        Advertisement.Initialize(gameId, testMod, this);
    }
    public void OnInitializationComplete()
    {
        debug_text.text = "Инициализация прошла успешно.";
        Debug.Log("Инициализация прошла успешно.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        debug_text.text = $"Ошибка инициализации: {error.ToString()} - {message}";
        Debug.Log($"Ошибка инициализации: {error.ToString()} - {message}");
    }
}
