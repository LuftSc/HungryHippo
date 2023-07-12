using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DevourTrigger : MonoBehaviour
{
    [Header("Ссылка на строку со счётом очков")]
    [SerializeField] private TextMeshProUGUI _counterWatermellows;
    
    public static int count = 0;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Watermellow"))
        {
            count++;
            _counterWatermellows.text = count.ToString();
            Destroy(col.gameObject);
        }
        
    }
}
