using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [Header("Название сцены, которую надо загрузить")]
    [SerializeField] private string SceneName;

    public void OnClick()
    {
        SceneManager.LoadScene(SceneName);
    }
}
