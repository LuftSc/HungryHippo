using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
	private static int ScreenWidth = Screen.width; 
	private static int ScreenHeight = Screen.height;
    // Start is called before the first frame update

    

    void Start()
    {
	    // 2560 1920 1280 640 (- 640)
	    // 1440 1080 720 360 (- 360)
	    // Время ставим в обычный темп
	    Time.timeScale = 1f;
	    // Обновляем счётчик
	    DevourTrigger.count = 0;
	    // Это для корректного сброса арбузов при начале игры
	    Dice.localCountWatermelow = 7;
	    Dice.ReSpawnWatermellow = true;
	    TimerLine.ReRunTimer = true; 
    }
    
}
