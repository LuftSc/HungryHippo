using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DestroyTrigger : MonoBehaviour
{
    [Header("Ссылки на жизни(сердечки)")] 
    [SerializeField] private Image Heart_1;
    [SerializeField] private Image Heart_2;
    [SerializeField] private Image Heart_3;

    [Header("Ссылка на меню, которое будет\nпоказано при проигрыше")]
    [SerializeField] private GameObject GameOverMenu;

    [Header("Ссылка на текст показа счёта\nв Game Over окне")] 
    [SerializeField] private TextMeshProUGUI ResultCount;

    public static Image[] Hearts;
    public static int _heartsIndex = 0;

    private AudioSource _audioWatermelow;
    private AudioSource _audioGameOver;

    private void Awake()
    {
        Hearts = new[] { Heart_3, Heart_2, Heart_1 };
        _heartsIndex = 0;
        _audioWatermelow = gameObject.GetComponent<AudioSource>();
        _audioGameOver = GameOverMenu.GetComponent<AudioSource>();
    }

    // Используется для удаления арбузов, которые бегемот не поймал
    // А так же для отнимания жизней бегемота за пропущенный арбуз.
    private void OnTriggerEnter2D(Collider2D col)
    {
        // Если упавший предмет - это арбуз
        if (col.gameObject.CompareTag("Watermellow"))
        {
            // Проигрываем звук падения арбуза
            _audioWatermelow.Play();
            
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
                // Через полсекунды оставливаем время
                Invoke("StartPlayGameOverSound", 0.3f);
                Invoke("StopTime", 0.4f);
                // Записываем количество собранных очков в результат
                ResultCount.text = DevourTrigger.count.ToString();
                // Показываем окошко Game Over
                GameOverMenu.SetActive(true);
                
            }
        }
        
        // Удаляем упавший арбуз
        Destroy(col.gameObject);
    }
    public void StartPlayGameOverSound()
    {
        // Проигрываем звук поражения
        _audioGameOver.Play();
    }
    void StopTime()
    {
        // Останавливаем время игры(ставим на паузу)
        Time.timeScale = 0f;
    }
    
}
