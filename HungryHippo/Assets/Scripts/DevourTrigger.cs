using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DevourTrigger : MonoBehaviour
{
    [Header("Ссылка на строку со счётом очков")]
    [SerializeField] private TextMeshProUGUI _counterWatermellows;

    [Header("Ссылка на поле, в котором будет\nпоказано кол-во очков после проигрыша")]
    [SerializeField] private TextMeshProUGUI ResultCount;
    [Header("Ссылка на окно поражения")]
    [SerializeField] private GameObject GameOverMenu;
    [Header("Звук съедания сердечка")]
    [SerializeField] private AudioSource HeartSound;
    [Header("Звук съеденного арбуза")] 
    [SerializeField] private AudioSource WatermelowSound;
    [Header("Ссылка на взрыв")] [SerializeField]
    private ParticleSystem Bang;
    [Header("Ссылки на коллайдеры бегемота")]
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private CapsuleCollider2D _capsuleCollider2D;

    [Header("Аниматор для съедания")]
    [SerializeField] private Animator _animator;
    
    private AudioSource _audioGameOver;
    private AudioSource _audioBomb;
    
    public static int count = 0;

    private void Awake()
    {
        _audioGameOver = GameOverMenu.GetComponent<AudioSource>();
        _audioBomb = gameObject.GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        // Если бегемот поймал что-то ртом
        if (col.IsTouching(_boxCollider2D))
        {
            _animator.SetBool("isEat", true);
            Invoke("SetAnimationInvoke", 0.4f);
            // Если бегемот съел арбуз
            if (col.gameObject.CompareTag("Watermellow"))
            {
                // Звук съеденного арбуза
                WatermelowSound.Play();
                // Для укорочения записис
                var localScale = gameObject.transform.localScale;
                // Если размер бегемота меньше максимального
                if (localScale.x <= 2.6f)
                {
                    // За каждый съеденный арбуз прибавляем 0.01 к размеру
                    gameObject.transform.localScale = new Vector3(localScale.x + 0.01f,localScale.y + 0.01f);
                }
            
                count++;
                _counterWatermellows.text = count.ToString();
                Destroy(col.gameObject);
            }
            
            // Если бегемот съел бомбу
                    if (col.gameObject.CompareTag("Bomb"))
                    {
                        Bang.gameObject.SetActive(true);
                        Invoke("StopBang", 0.5f);
                        Destroy(col.gameObject);
                        // Проигрываем звук взрыва бомбы
                        _audioBomb.Play();
                        // Если бомба съедена и количество жизней > 1
                        if (DestroyTrigger._heartsIndex < DestroyTrigger.Hearts.Length - 1)
                        {
                            DestroyTrigger.Hearts[DestroyTrigger._heartsIndex].enabled = false;
                            DestroyTrigger._heartsIndex++;
                        }
                        // Если осталась 1 жизнь и бегемот съел бомбу (Конец игры)
                        else
                        {
                            DestroyTrigger.Hearts[DestroyTrigger._heartsIndex].enabled = false;
                            // Через полсекунды оставливаем время
                            Invoke("StartPlayGameOverSound", 0.3f);
                            Invoke("StopTime", 0.4f);
                            // Записываем количество собранных очков в результат
                            ResultCount.text = count.ToString();
                            // Показываем окошко Game Over
                            GameOverMenu.SetActive(true);
                        }
                    }
            
                    if (col.gameObject.CompareTag("Heart"))
                    {
                        // Проигрываем звук подбора сердечка
                        HeartSound.Play();
                        // Если все 3 сердечка целые, игрок получает 10 очков
                        if (DestroyTrigger._heartsIndex == 0)
                        {
                            count += 10;
                            _counterWatermellows.text = count.ToString();
                        }
                        else
                        {
                            // Восстанавливаем отсутсвующее сердечко
                            DestroyTrigger._heartsIndex -= 1;
                            DestroyTrigger.Hearts[DestroyTrigger._heartsIndex].enabled = true;
                        }
                        
                        Destroy(col.gameObject);
                    }
                    
        }

        // Если бегемот поймал что-то пузом
        if (col.IsTouching(_capsuleCollider2D))
        {
            // Если бегемот съел бомбу
            if (col.gameObject.CompareTag("Bomb"))
            {
                Bang.gameObject.SetActive(true);
                Invoke("StopBang", 0.5f);
                Destroy(col.gameObject);
                // Проигрываем звук взрыва бомбы
                _audioBomb.Play();
                // Если бомба съедена и количество жизней > 1
                if (DestroyTrigger._heartsIndex < DestroyTrigger.Hearts.Length - 1)
                {
                    DestroyTrigger.Hearts[DestroyTrigger._heartsIndex].enabled = false;
                    DestroyTrigger._heartsIndex++;
                }
                // Если осталась 1 жизнь и бегемот съел бомбу (Конец игры)
                else
                {
                    DestroyTrigger.Hearts[DestroyTrigger._heartsIndex].enabled = false;
                    // Через полсекунды оставливаем время
                    Invoke("StartPlayGameOverSound", 0.3f);
                    Invoke("StopTime", 0.4f);
                    // Записываем количество собранных очков в результат
                    ResultCount.text = count.ToString();
                    // Показываем окошко Game Over
                    GameOverMenu.SetActive(true);
                }
            }
        }
    }

    public void SetAnimationInvoke()
    {
        _animator.SetBool("isEat", false);
    }
    public void StartPlayGameOverSound()
    {
        // Проигрываем звук поражения
        _audioGameOver.Play();
    }
    public void StopTime()
    {
        Time.timeScale = 0f;
    }

    public void StopBang()
    {
        Bang.gameObject.SetActive(false);
    }
}
