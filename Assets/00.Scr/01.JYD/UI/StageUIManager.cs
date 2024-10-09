using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageUIManager : MonoBehaviour
{
    
    public string currentScene;

    [SerializeField] private TextMeshProUGUI mainText;
    [SerializeField] private GameObject menu;
    [SerializeField] private TMPro.TMP_Text text;
    [SerializeField] private Image _img;
    [SerializeField] private Color startedColor;
    [SerializeField] private Color stoppedColor;
    
    [SerializeField] private Transform player;
    private Stage nextStage;
    
    public static event Action<bool> OnSceneChange;
    private void Start()
    {
        OnTimeToggle(false);
        //string curSceneName = "Map";// SceneManager.GetActiveScene().name;
        BshAmiKlr.GameManager.Set(false);
        OnSceneChange?.Invoke(false);
    }
    public void SetScene(string str)
    {
        if (str == "Highway")
        {
            mainText.SetText("Enter Highway");
            print(mainText.text);
            currentScene = string.Empty;
            return;
        }
        
        mainText.SetText("Entering");
        currentScene = str;
    }
    public void UpdateTime(int hour, float minute)
    {
        text.text = $"Current Time: h:{hour}, m: {minute}";
    }
    public void OnTimeToggle(bool value)
    {
        if (value) _img.color = startedColor;
        else       _img.color = stoppedColor;
    }
    public void SuccessScene()
    {
        if (currentScene == String.Empty && nextStage != null)
        {
            player.transform.position = nextStage.transform.position;
            SetActive(false);
            return;
        }

        OnSceneChange?.Invoke(true);
        BshAmiKlr.GameManager.Set(true);
        SceneManager.LoadScene(currentScene);
    }

    public void FailedScene()
    {
        if (nextStage != null)
            nextStage = null;
        
        SetActive(false);
    }

    public void SetActive(bool b)
    {
        menu.SetActive(b);
    }

    public void SetNextStage(Stage _nextStage)
    {
        nextStage = _nextStage;
    }
}
