using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
	[Header("Ссылки на трансформы левого кубика и правого")]
	[SerializeField] private Transform LeftDice;
	[SerializeField] private Transform RightDice;
	
	void Start()
    {
	    // Эта вся штука для небольшого сдвига игральных костей
	    // для девайсов, у которых разрешение (высота / ширину) больше 2 (1520/720)
	    var height = (float)Screen.height;
	    var width = (float)Screen.width;
	    var k = height / width;
	    if (k == 2.0f)
	    {
		    LeftDice.position = new Vector3(-0.8f, 0.04f);
		    RightDice.position = new Vector3(0.8f, 0.04f);
	    }
	    else if (k > 2.0f)
	    {
		    var mod = k - 2;
		    var intPart = Convert.ToInt32(mod / 0.0186);
		    //Debug.Log($"intPart{intPart}");
		    var newX = (float)(0.8 - intPart / 100.0f);
		    LeftDice.position = new Vector3(-newX, 0.04f);
		    RightDice.position = new Vector3(newX, 0.04f);
		    //Debug.Log($"newX {newX}");
	    }
	    
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
