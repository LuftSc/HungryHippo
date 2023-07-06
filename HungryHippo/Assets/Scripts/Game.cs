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
 
	    if(ScreenWidth == Screen.width || ScreenHeight == Screen.height)
	    {
		    ScreenHeight = Screen.width - (640 * 2);
		    ScreenWidth = Screen.height - (360 * 2);
		    Screen.SetResolution(ScreenWidth, ScreenHeight, true);
	    }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
