using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Game : MonoBehaviour
{
	[Header("Ссылка на игральный кубик")] 
	[SerializeField] private Dice _dice;
	void Start()
	{
		//if (SkipLearning.skip) Time.timeScale = 1f;
	    // Время ставим в обычный темп
	    //Time.timeScale = 1f;
	    // После перезапуска сцены начнём с одного арбуза
	    Dice.isFirst = true;
	    // Обновляем счётчик
	    DevourTrigger.count = 0;
	    // Это для корректного сброса арбузов при начале игры
	    _dice.localCountWatermelow = 7;
	    _dice.ReSpawnWatermellow = true;
	    TimerLine.ReRunTimer = true; 
    }
}
