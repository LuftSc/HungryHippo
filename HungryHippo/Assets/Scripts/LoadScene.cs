using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [Header("Название сцены, которую надо загрузить")]
    [SerializeField] private string SceneName;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void OnClick()
    {
        Time.timeScale = 1f;
        _audioSource.Play();
        SceneManager.LoadScene(SceneName);
    }
}
