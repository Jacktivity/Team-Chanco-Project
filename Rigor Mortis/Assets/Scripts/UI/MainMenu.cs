using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Canvas mainCanvas;
    public Canvas levelSelectCanvas;

    [SerializeField]private TextAsset loadedScene;

    private static EventHandler<MainMenuStates> mainMenuStateChange;

    MainMenuStates currentState;

    public GameObject levelDetailsImage;

    public Sprite empty, highGroundInfo, bottleneckInfo, threefoldInfo, shootinggalleryInfo, labyrinthInfo, gauntletInfo;

    private void Start()
    {
        mainMenuStateChange += MainMenuStateChanged;
        mainMenuStateChange?.Invoke(this, MainMenuStates.mainCanvas);

        levelDetailsImage.SetActive(false);
    }

    public enum MainMenuStates
    {
        mainCanvas, newGame, loadGame, levelSelect, settings, quit
    }

    public void MainCanvasButton()
    {
        mainMenuStateChange?.Invoke(this, MainMenuStates.mainCanvas);
    }

    public void LevelSelectCanvasButton()
    {
        mainMenuStateChange?.Invoke(this, MainMenuStates.levelSelect);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLevel(TextAsset level)
    {
        loadedScene = level;
        /*switch(level)
        {
            case 1:
                levelDetailsImage.SetActive(true);
                levelDetailsImage.GetComponent<Image>().sprite = highGroundInfo;
            break;

            case 2:
                levelDetailsImage.SetActive(false);
                break;

            default:
                levelDetailsImage.SetActive(false);
            break;
        }*/
    }

    public void PlayButton()
    {
        PersistantData.level = loadedScene;
        PersistantData.levelAssigned = true;
        SceneManager.LoadScene(1);
    }

    private void MainMenuStateChanged(object sender, MainMenuStates state)
    {
        loadedScene = null;
            switch (state)
            {
                case MainMenuStates.mainCanvas:
                    currentState = MainMenuStates.newGame;

                    SetMainCanvas(true);
                    SetLevelSelectCanvas(false);
                    break;

            case MainMenuStates.newGame:
                    currentState = MainMenuStates.newGame;

                    SetMainCanvas(true);
                    SetLevelSelectCanvas(false);
                    break;

                case MainMenuStates.loadGame:
                    currentState = MainMenuStates.loadGame;

                    SetMainCanvas(false);
                    SetLevelSelectCanvas(false);
                    break;

                case MainMenuStates.levelSelect:
                    currentState = MainMenuStates.levelSelect;

                    SetMainCanvas(false);
                    SetLevelSelectCanvas(true);
                    break;

                case MainMenuStates.quit:
                    currentState = MainMenuStates.quit;

                    SetMainCanvas(false);
                    SetLevelSelectCanvas(false);
                    break;
            }
        }

    public void SetMainCanvas(bool enabled)
    {
        if (!mainCanvas.gameObject.activeSelf && enabled)
        {
            mainCanvas.gameObject.SetActive(true);
        }

        if (mainCanvas.enabled != enabled)
        {
            mainCanvas.enabled = enabled;
        }
    }

    public void SetLevelSelectCanvas(bool enabled)
    {
        if (!levelSelectCanvas.gameObject.activeSelf && enabled)
        {
            levelSelectCanvas.gameObject.SetActive(true);
        }

        if (levelSelectCanvas.enabled != enabled)
        {
            levelSelectCanvas.enabled = enabled;
        }
    }
}
