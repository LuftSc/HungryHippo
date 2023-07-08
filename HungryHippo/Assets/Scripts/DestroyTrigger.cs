using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyTrigger : MonoBehaviour
{
    [Header("Ссылки на жизни(сердечки)")] 
    [SerializeField] private Image Heart_1;
    [SerializeField] private Image Heart_2;
    [SerializeField] private Image Heart_3;

    private Image[] Hearts;
    private int _heartsIndex = 0;

    private void Awake()
    {
        Hearts = new[] { Heart_3, Heart_2, Heart_1 };
    }

    // Используется для удаления арбузов, которые бегемот не поймал
    // А так же для отнимания жизней бегемота за пропущенный арбуз.
    private void OnTriggerEnter2D(Collider2D col)
    {
        // Если арбуз был не пойман и количество жизней > 1
        if (_heartsIndex < Hearts.Length - 1)
        {
            Hearts[_heartsIndex].enabled = false;
            _heartsIndex++;
        }
        // Если осталась 1 жизнь и арбуз был пропущен (Конец игры)
        else
        {
            Hearts[_heartsIndex].enabled = false;
            Debug.Log("GAME OVER!");
        }
        // Удаляем упавший арбуз
        Destroy(col.gameObject);
    }
}
