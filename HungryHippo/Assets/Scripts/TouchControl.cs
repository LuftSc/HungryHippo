using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchControl : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] private Transform Hippopotam;
    // 10,5 %
    [SerializeField] private int PixelsForHippoY;

    public void OnBeginDrag(PointerEventData eventData)
    {
        /*
        if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
        {
            if (eventData.delta.x > 0)
            {
                Hippopotam.position += Vector3.right;
            }
            else
            {
                Hippopotam.position += Vector3.left;
            }
        } */
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Hippopotam.position =
            Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, PixelsForHippoY, 1));
    }
}
