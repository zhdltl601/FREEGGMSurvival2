using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class StageUIManager : MonoBehaviour
{
    public string currentScene;
    [SerializeField] private GameObject menu;

    public void SetScene(string str)
    {
        currentScene = str;
    }

    public void SuccessScene()
    {
        SceneManager.LoadScene(currentScene);
    }

    public void FailedScene()
    {
        SetActive(false);
    }

    public void SetActive(bool b)
    {
        menu.SetActive(b);
    }
}
