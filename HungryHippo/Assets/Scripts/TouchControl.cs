using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchControl : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [Header("Ссылка на трансформ бегемота")]
    [SerializeField] private Transform Hippopotam;

    [Header("Ссылка на трансформ тачера")] 
    [SerializeField] private Transform Toucher;
    // 10,5 %
    //[Header("Значение (в процентах 0.0 - 1.0)\nвертикальной позиции бегемота")]
    //[SerializeField] private float PixelsForHippoPercentsY;

    private int HippoPositionY;
    private int ToucherPositionY;

    private void Awake()
    {
        HippoPositionY = (int)(0.18 * Screen.height);
        ToucherPositionY = (int)(0.06 * Screen.height);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }
    public void OnDrag(PointerEventData eventData)
    {
        // Рабочий вариант
        /*Hippopotam.position =
            Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, HippoPositionY, 1)); */
        
        // Моя импровизация
        var vector = new Vector3(Input.mousePosition.x, ToucherPositionY, 1);
        var toucherPosition = Camera.main.ScreenToWorldPoint(vector);
        //Debug.Log(vector.x);
        if (toucherPosition.x > -1.8f && toucherPosition.x < 1.8f)
        {
            Toucher.position = toucherPosition;
            Hippopotam.position = new Vector3(Toucher.position.x, Hippopotam.position.y, 1);
        }
    }
}
