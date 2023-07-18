using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class DevourTrigger : MonoBehaviour
{
    //[Header("Ссылка на строку со счётом очков")]
    //[SerializeField] private TextMeshProUGUI _counterWatermellows;
    [Header("Ссылка на панельку с отображаемыми очками")] 
    [SerializeField] private GameObject VisualCount;

    [Header("Ссылка на поле, в котором будет\nпоказано кол-во очков после проигрыша")]
    [SerializeField] private TextMeshProUGUI ResultCount;
    [Header("Ссылка на окно поражения")]
    [SerializeField] private GameObject GameOverMenu;
    [Header("Звук съедания сердечка")]
    [SerializeField] private AudioSource HeartSound;
    [Header("Звук съеденного арбуза")] 
    [SerializeField] private AudioSource WatermelowSound;
    [Header("Звук съеденной капусты")]
    [SerializeField] private AudioSource CabbageSound;
    [Header("Звук отскока кокоса")]
    [SerializeField] private AudioSource CocountSound;
    [Header("Ссылка на взрыв")] [SerializeField]
    private ParticleSystem Bang;
    [Header("Ссылка на изображение\nраскрытого кокоса")] 
    [SerializeField] private Sprite openedCocount;
    [Header("Ссылки на коллайдеры бегемота")]
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private CapsuleCollider2D _capsuleCollider2D;

    [Header("Аниматор для съедания")]
    [SerializeField] private Animator _animator;
    
    private AudioSource _audioGameOver;
    private AudioSource _audioBomb;
    private TextMeshProUGUI _counterWatermellows;
    public static int count = 0;

    private void Awake()
    {
        _audioGameOver = GameOverMenu.GetComponent<AudioSource>();
        _audioBomb = gameObject.GetComponent<AudioSource>();
        _counterWatermellows = VisualCount.transform.Find("Count").gameObject.GetComponent<TextMeshProUGUI>();
        VisualCount.SetActive(true);
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
                SetHippoSize(WatermelowSound, 0.005f);
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
                            VisualCount.SetActive(false);
                        }
                    }
            // Если бегемот съел сердечко
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
            // Если бегемот поймал кокос
            if (col.gameObject.CompareTag("Cocount"))
            {
                // Если бегемот поймал нерасколотый кокос
                if (!col.gameObject.GetComponent<TrailRenderer>().enabled)
                {
                    // Проигрываем звук отскока кокоса
                    CocountSound.Play();
                    // Создаём новый кокос (расколотый)
                    var newCocount = Instantiate(col.gameObject,
                        new Vector3(col.gameObject.transform.position.x, col.gameObject.transform.position.y),
                        Quaternion.identity);
                    // Добавляем ему молочный след
                    var trail = newCocount.GetComponent<TrailRenderer>();
                    trail.enabled = true;
                    // Устанавливаем картинку раскрытого кокоса
                    var sr = newCocount.gameObject.GetComponent<SpriteRenderer>();
                    sr.sprite = openedCocount;
                    // Подбрасываем вверх
                    var rb = newCocount.gameObject.GetComponent<Rigidbody2D>();
                    rb.AddForce(new Vector2(0,2) * 250);
                }
                // Если бегемот съел уже расколотый кокос
                else
                {
                    SetHippoSize(WatermelowSound, 0.025f);
                    count += 5;
                    _counterWatermellows.text = count.ToString();
                    Destroy(col.gameObject);
                }
            }
            // Если бегемот поймал капусту
            if (col.gameObject.CompareTag("Cabbage"))
            {
                SetHippoSize(CabbageSound, -0.1f);
                count += 3;
                _counterWatermellows.text = count.ToString();
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

    // Метод для увеличения размера бегемота,
    // sound - звук при поедании предмета
    // scaleIncrement - на сколько увеличивать размер
    private void SetHippoSize(AudioSource sound, float scaleIncrement)
    {
        // Звук съеденного предмета
        sound.Play();
        // Для укорочения записи
        var localScale = gameObject.transform.localScale;
        // Чтобы бегемот не уменьшился меньше изначального размера
        if (scaleIncrement < 0 && localScale.x + scaleIncrement < 1f) return;
        // Если размер бегемота меньше максимального
        if (localScale.x <= 2.6f)
        {
            // За каждый съеденный предмет прибавляем scaleIncrement к размеру
            gameObject.transform.localScale = new Vector3(localScale.x + scaleIncrement,localScale.y + scaleIncrement);
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
