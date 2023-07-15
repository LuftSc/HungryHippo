using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PopupMenu : MonoBehaviour
{
    [Header("Ссылка на попап, который будет \nпоказан при нажатии на кнопку меню")]
    [SerializeField] private GameObject MenuPopup;

    [Header("Ссылка на звук для кнопки ПРОДОЛЖИТЬ")] 
    [SerializeField] private AudioSource ContinueSound;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    // При клике на менюшку
    public void OnMenuClick()
    {
        // Проигрываем музыку
        _audioSource.Play();
        
        MenuPopup.SetActive(true);
        StopTime();
    }
    // При клике на кнопку ПРОДОЛЖИТЬ
    public void OnContinueClick()
    {
        MenuPopup.SetActive(false);
        Time.timeScale = 1f;
        //Invoke("StartTime", 0.1f);
        
        // Проигрываем музыку
        ContinueSound.Play();
    }
    // Останавливаем игровое время
    void StopTime()
    {
        Time.timeScale = 0f;
    }
    //Запускаем Игровое время
    void StartTime()
    {
        Time.timeScale = 1f;
    }
}
