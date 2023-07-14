using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [Header("Изображение для активной кнопки")]
    [SerializeField] private Sprite SoundOn;

    [Header("Изображение для выключенной кнопки")] 
    [SerializeField] private Sprite SoundOff;

    private static bool isOn = true;
    private UnityEngine.UI.Image _currentlyImage;
    private AudioSource _audioSource;
    private void Awake()
    {
        _currentlyImage = gameObject.GetComponent<UnityEngine.UI.Image>();
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void OnClick()
    {
        // Проигрываем клик кнопки
        _audioSource.Play();
        // Если кнопка активна
        if (isOn)
        {
            // Меняем картинку
            _currentlyImage.sprite = SoundOff;
            // Устанавливаем громкость на 0
            AudioListener.volume = 0f;
        }
        else
        {
            // Меняем картинку
            _currentlyImage.sprite = SoundOn;
            // Устанавливаем громкость на 1
            AudioListener.volume = 1f;
        }

        isOn = !isOn;
    }
}
