using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContinueGameAfterAd : MonoBehaviour
{
    [Header("Ссылка на окошко поражения")]
    [SerializeField] private GameObject GameOverPopup;

    [Header("Ссылка на окошко со счётом")] 
    [SerializeField] private GameObject CountWindow;

    [Header("Ссылка на объект рекламы")]
    [SerializeField] private RewardedAdsButton _rewardedAdsButton;

    private AudioSource _sound;

    private void Start()
    {
        _sound = gameObject.GetComponent<AudioSource>();
    }

    public void OnClick()
    {
        _sound.Play();
        _rewardedAdsButton.ShowAd();
        foreach (var heart in DestroyTrigger.Hearts)
        {
            heart.enabled = true;
        }

        DestroyTrigger._heartsIndex = 0;
        CountWindow.SetActive(true);

        DeleteAllBulletObjects(GetAllBulletObjects(new string[] {"Bomb", "Watermellow", "Heart", "Cocount", "Cabbage"}));
        
        GameOverPopup.SetActive(false);
        
    }
    private GameObject[] GetAllBulletObjects(string[] tags)
    {
        var allBulletObjects = new GameObject[] { };
        foreach (var tag in tags)
        {
            allBulletObjects = allBulletObjects.Concat(GameObject.FindGameObjectsWithTag(tag)).ToArray();
        }
        return allBulletObjects;
    }

    private void DeleteAllBulletObjects(GameObject[] objects)
    {
        foreach (var obj in objects)
        {
            Destroy(obj);
        }
    }
}
