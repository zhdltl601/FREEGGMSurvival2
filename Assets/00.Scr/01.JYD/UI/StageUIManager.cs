using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageUIManager : MonoBehaviour
{
    public string currentScene;
    [SerializeField] private GameObject menu;
    [SerializeField] private TMPro.TMP_Text text;
    public void SetScene(string str)
    {
        currentScene = str;
    }
    public void UpdateTime(int hour, float minute)
    {
        text.text = $"Current Time: h:{hour}, m: {minute}";
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
