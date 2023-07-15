using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using FixedUpdate = UnityEngine.PlayerLoop.FixedUpdate;
using Random = System.Random;

public class Dice : MonoBehaviour
{
    [Header("Ссылки на изображения граней\nигрального кубика")]
    [SerializeField] private Sprite Dice_1;
    [SerializeField] private Sprite Dice_2;
    [SerializeField] private Sprite Dice_3;
    [SerializeField] private Sprite Dice_4;
    [SerializeField] private Sprite Dice_5;
    [SerializeField] private Sprite Dice_6;

    [Header("Ссылка на префаб арбуза")]
    [SerializeField] private GameObject Watermellow;

    [Header("Ссылка на префаб бомбы")] 
    [SerializeField] private GameObject Bomb;

    [Header("Ссылка на префаб сердечка")] 
    [SerializeField] private GameObject Heart;

    [Header("Ссылка на префаб кокоса")] 
    [SerializeField] private GameObject Cocount;

    [Header("Ссылка на префаб капусты")] 
    [SerializeField] private GameObject Cabbage;

    [Header("Разброс арбузов в радиусе: ")]
    [SerializeField] private int LeftBoard;
    [SerializeField] private int RightBoard;

    public int localCountWatermelow = 0;

    private float _positionX;
    private Sprite[] Dices;
    private Random random;
    private int randomInt;
    private SpriteRenderer _spriteRenderer;
    private GameObject Bullet;
    private AudioSource _audioSource;
    private Animator _animator;
    
    public static bool isFirst = true;
    // Start is called before the first frame update
    void Start()
    {
        // Позиция по иксу для спавна в этой точке арбузов
        _positionX = gameObject.transform.position.x;
        
        // По умолчанию будем стрелять арбузами
        Bullet = Watermellow;
        
        Dices = new[] { Dice_1, Dice_2, Dice_3, Dice_4, Dice_5, Dice_6 };
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        random = new Random();
        // При первом запуске игры начнём с 1 предмета
        randomInt = isFirst ? 0: random.Next(0, 5);
        isFirst = !isFirst;
        
        _animator = gameObject.GetComponent<Animator>();
        // Проигрываем анимацию вращения
        _animator.SetBool("goRotate", true);
        Invoke("StopRotate", 0.3f);
        
        // Присваиваем новое изображение кубика
        _spriteRenderer.sprite = Dices[randomInt];
        _audioSource = gameObject.GetComponent<AudioSource>();
    }
    //точность до милисекунды (параметры для таймера)
    private float nextActionTime = 0.0f;
    // Период совершаемых таймером действий (в секундах) (0.5f) (timer 3sec)
    private float period = 0.5f;
    // Флаг для начала респауна арбузов
    public bool ReSpawnWatermellow = false;
    private void FixedUpdate()
    {
        if (Time.time > nextActionTime) {
            // Пока количество созданных арбузов (в рамках одного таймера)
            // меньше, чем число на кубике
            if (localCountWatermelow < randomInt + 1)
            {
                nextActionTime += period;
                // С шансом 30% создаём бомбу
                if (random.Next(0, 101) <= 30) Bullet = Bomb;
                // С шансом 10% создаём кокос
                if (random.Next(0, 101) <= 10) Bullet = Cocount;
                // С шансом 6% создаём капусту
                if (random.Next(0, 101) <= 6) Bullet = Cabbage;
                // С шансом 5% создаём сердечко
                if (random.Next(0, 101) <= 5) Bullet = Heart;
                // Создаём Ядро( либо арбуз, либо бомбу, либо сердечко)
                var bullet = Instantiate(Bullet, new Vector3(_positionX,0), Quaternion.identity);
                var rb = bullet.GetComponent<Rigidbody2D>();
                
                // Проигрываем звук выброса предмета
                _audioSource.Play();
                
                // Генерируем дробное число в пределах от
                // -25/100 до 25/100. (центр)
                // -37/100 do 14/100 (право)
                // -14/100 do 37/100 (лево)
                var randDirection = (float)random.Next(LeftBoard, RightBoard);
                var resDirection = randDirection == 0 ? 0f : randDirection / 100;
                // Выстреливаем арбузом
                rb.AddForce(new Vector2(resDirection,2) * 200);
                
                localCountWatermelow++;

                Bullet = Watermellow;
            }
            // Этот иф срабатывает когда завершается действие таймера
            else if (ReSpawnWatermellow)
            {
                // Записываем значения таймера к изначальному варианта
                ReSpawnWatermellow = false;
                localCountWatermelow = 0;
                nextActionTime = Time.time;
                period = 0.5f;
                // Обновляем число на кубике
                randomInt = random.Next(0, 6);
                _animator.SetBool("goRotate", true);
                Invoke("StopRotate", 0.4f);
                _spriteRenderer.sprite = Dices[randomInt];
            }
            
        }
    }

    public void StopRotate()
    {
        _animator.SetBool("goRotate", false);
    }
}
