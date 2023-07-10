using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PopupMenu : MonoBehaviour
{
    [Header("Ссылка на попап, который будет \nпоказан при нажатии на кнопку меню")]
    [SerializeField] private GameObject MenuPopup;
    // При клике на менюшку
    public void OnMenuClick()
    {
        MenuPopup.SetActive(true);
        StopTime();
    }
    // При клике на кнопку ПРОДОЛЖИТЬ
    public void OnContinueClick()
    {
        MenuPopup.SetActive(false);
        Time.timeScale = 0.3f;
        Invoke("StartTime", 0.1f);
    }
    // Останавливаем игровое время
    void StopTime()
    {
        Time.timeScale = 0f;
    }
    //Запускаем Игровое время
    void StartTime()
    {
        Time.timeScale = 1f;
    }
}