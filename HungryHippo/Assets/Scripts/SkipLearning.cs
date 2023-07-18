using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkipLearning : MonoBehaviour
{
    public static bool showLearning = true;
    private void Start()
    {
        // Если нужно показывать экран обучения
        if (showLearning)
        {
            // Показываем экран обучения
            gameObject.SetActive(true);
            // Останавливаем время, чтобы игрок прочитал обучение
            Time.timeScale = 0f;

            showLearning = false;
        }
        // Если НЕ нужно показывать экран обучения
        else
        {
            gameObject.SetActive(false);
        }
    }
    public void OnClick()
    {
        // Убираеим обучение с экрана
        gameObject.SetActive(false);
        // Восстанавливаем нормальный ход времени
        Time.timeScale = 1f;
        showLearning = false;
    }
}
