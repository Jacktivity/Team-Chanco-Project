using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    Canvas MainCanvas;
    Canvas LevelSelectCanvas;

    private void Awake()
    {
        MainCanvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
        LevelSelectCanvas = GameObject.Find("LevelSelectCanvas").GetComponent<Canvas>();
    }

    public void Play()
    {
        MainCanvas.enabled = false;
        LevelSelectCanvas.enabled = true;
    }

    public void Back()
    {
        MainCanvas.enabled = true;
        LevelSelectCanvas.enabled = false;
    }

    //Button Quit Method
    public void Quit()
    {
        Application.Quit();
    }
}
