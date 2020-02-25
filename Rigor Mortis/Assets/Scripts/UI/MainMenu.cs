using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Canvas mainCanvas, levelSelectCanvas, backgroundCanvas;

    [SerializeField] private TextAsset level1, level2, level3, level4, level5, level6;
    [SerializeField]private TextAsset loadedScene;

    public static EventHandler<MainMenuStates> mainMenuStateChange;

    public MainMenuStates currentState;

    public GameObject levelDetailsImage;

    public Sprite empty, level1Info, level2Info, level3Info, level4Info, level5Info, level6Info;

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

    public void LevelEditorCanvasButton()
    {
        PersistantData.level = loadedScene;
        PersistantData.levelAssigned = true;

        if (!SceneManager.GetSceneByBuildIndex(1).isLoaded)
        {
            //SceneManager.LoadScene(1, LoadSceneMode.Additive);
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }

        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLevel(int level)
    {
        switch(level)
        {
            case 1:
                levelDetailsImage.SetActive(true);
                loadedScene = level1;
                levelDetailsImage.GetComponent<Image>().sprite = level1Info;
                break;

            case 2:
                levelDetailsImage.SetActive(false);
                loadedScene = level2;
                break;

            default:
                levelDetailsImage.SetActive(false);
            break;
        }
    }

    public void PlayButton()
    {
        PersistantData.level = loadedScene;
        PersistantData.levelAssigned = true;

        if (!SceneManager.GetSceneByBuildIndex(2).isLoaded) {
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
        }

        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        mainMenuStateChange?.Invoke(this, MainMenuStates.quit);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene.name));
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
                SetBackgroundCanvas(true);
                break;

        case MainMenuStates.newGame:
                currentState = MainMenuStates.newGame;

                SetMainCanvas(true);
                SetLevelSelectCanvas(false);
                SetBackgroundCanvas(true);

                break;

            case MainMenuStates.loadGame:
                currentState = MainMenuStates.loadGame;

                SetMainCanvas(false);
                SetLevelSelectCanvas(false);
                SetBackgroundCanvas(true);

                break;

            case MainMenuStates.levelSelect:
                currentState = MainMenuStates.levelSelect;

                SetMainCanvas(false);
                SetLevelSelectCanvas(true);
                SetBackgroundCanvas(true);

                break;

            case MainMenuStates.quit:
                currentState = MainMenuStates.quit;

                SetMainCanvas(false);
                SetLevelSelectCanvas(false);
                SetBackgroundCanvas(false);

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
    public void SetBackgroundCanvas(bool enabled) {
        if (!backgroundCanvas.gameObject.activeSelf && enabled) {
            backgroundCanvas.gameObject.SetActive(true);
        }

        if (backgroundCanvas.enabled != enabled) {
            backgroundCanvas.enabled = enabled;
        }
    }
}
