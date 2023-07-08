using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TreeEditor;
using UnityEngine;
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
    
    private Sprite[] Dices;
    private Random random;
    private int randomInt;
    private int localCountWatermelow = 0;

    private SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
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

    //точность до милисекунды (параметры для таймера)
    private float nextActionTime = 0.0f;
    // Период совершаемых таймером действий (в секундах)
    private float period = 0.5f;
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
                var positionX = GetRandomCoordinateX(-2, 2, 0.5);
        
                // Создаём арбуз
                Instantiate(Watermellow, new Vector3(positionX,3), Quaternion.identity);
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
