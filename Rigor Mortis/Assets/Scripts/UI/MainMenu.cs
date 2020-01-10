using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Canvas mainCanvas;
    public Canvas levelCanvas;

    private void Start()
    {
        levelCanvas.enabled = false;
    }

    public void CanvasSwitch()
    {
        levelCanvas.enabled = !levelCanvas.enabled;
        mainCanvas.enabled = !mainCanvas.enabled;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }
}
