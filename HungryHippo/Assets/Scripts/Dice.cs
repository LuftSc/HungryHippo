using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;
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

    [Header("Разброс арбузов в радиусе: ")]
    [SerializeField] private int LeftBoard;
    [SerializeField] private int RightBoard;


    private float _positionX;
    private Sprite[] Dices;
    private Random random;
    private int randomInt;
    public static int localCountWatermelow = 0;

    private SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        // Позиция по иксу для спавна в этой точке арбузов
        _positionX = gameObject.transform.position.x;
        
        Dices = new[] { Dice_1, Dice_2, Dice_3, Dice_4, Dice_5, Dice_6 };
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        random = new Random();
        
        randomInt = random.Next(0, 5);
        _spriteRenderer.sprite = Dices[randomInt];
    }
    // Возвращает дробное число в диапазоне [-randInt - doubleDifference; randInt + doubleDifference]
    public float GetRandomCoordinateX(int intLeft, int intRight, double doubleDifference)
    {
        // Генерируем рандомную дробную часть.
        var randDouble = random.NextDouble();
        randDouble = randDouble >= 0 ? randDouble - doubleDifference : randDouble + doubleDifference;
        
        // Генерируем рандомную целую часть
        var randInt = random.Next(intLeft, intRight);
        
        // Складываем и получаем число в диапазоне
        // [-randInt - doubleDifference; randInt + doubleDifference]
        randDouble = randInt >= 0 ? randDouble + randInt : randInt - randDouble;
        
        return (float)randDouble;
    }

    // Получаем Рандомную координату в диапазоне (-board; board);
    public float GetRandomCoordinateX(uint board)
    {
        var rDouble = 0.0;
        for (int i = 0; i < board; i++)
        {
            rDouble += random.NextDouble();
        }
        // Полуаем знак (плюс(0) иили минус(1))
        var sign = random.Next(0, 2);
        return (float)(sign == 0 ? rDouble : -rDouble);
    }

    //точность до милисекунды (параметры для таймера)
    private float nextActionTime = 0.0f;
    // Период совершаемых таймером действий (в секундах) (0.5f) (timer 3sec)
    private float period = 1.0f;
    // Флаг для начала респауна арбузов
    public static bool ReSpawnWatermellow;
    void Update () {
        if (Time.time > nextActionTime) {
            // Пока количество созданных арбузов (в рамках одного таймера)
            // меньше, чем число на кубике
            if (localCountWatermelow < randomInt + 1)
            {
                nextActionTime += period;
                // Получаем позицию арбуза по оси Х. По оси У она константная и равна высоте кубика
                //var positionX = GetRandomCoordinateX(2); Было по игреку было 3.
                // var positionX = 0.9f; // По умолчанию 0
                // Создаём арбуз
                var watermellow = Instantiate(Watermellow, new Vector3(_positionX,0), Quaternion.identity);
                var rb = watermellow.GetComponent<Rigidbody2D>();
                // Генерируем дробное число в пределах от
                // -25/100 до 25/100. (центр)
                // -37/100 do 14/100 (право)
                // -14/100 do 37/100 (лево)
                var randDirection = (float)random.Next(LeftBoard, RightBoard);
                var resDirection = randDirection == 0 ? 0f : randDirection / 100;
                // Выстреливаем арбузом
                rb.AddForce(new Vector2(resDirection,2) * 200);
                
                localCountWatermelow++;
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
                _spriteRenderer.sprite = Dices[randomInt];
            }
            
        }
    }
}
