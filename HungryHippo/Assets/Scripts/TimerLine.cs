using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerLine : MonoBehaviour
{
    [Header("Время(в секундах) для таймера")]
    [SerializeField] private float time;
    [Header("Ссылка на картинкy таймера")]
    [SerializeField] private Image timerImage;
    [Header("Ссылка на игральный кубик")]
    [SerializeField] private Dice _dice;

    public static bool ReRunTimer = false;
    private float _timeLeft = 0f;
    
    // Этот метод нужен для графического представления таймера (белая окружность)
    private IEnumerator StartTimer()
    {
        while (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
            var normalizedValue = Mathf.Clamp(_timeLeft / time, 0.0f, 1.0f);
            timerImage.fillAmount = normalizedValue;
            yield return null;
        }
        // Когда полоска подошла к концу, даём сигнал,
        // что пора запускать кубик и спавн арбузов ещё раз
        _dice.ReSpawnWatermellow = true;
        ReRunTimer = true;
        
    }
    private void Start()
    {
        _timeLeft = time;
        StartCoroutine(StartTimer());
    }

    private void Update()
    {
        if (ReRunTimer)
        {
            ReRunTimer = false;
            _timeLeft = time;
            StartCoroutine(StartTimer());
        }
    }
}
