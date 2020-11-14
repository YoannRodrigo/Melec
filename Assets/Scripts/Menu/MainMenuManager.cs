using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Canvas mainCanvas;
    public GameObject mainMenu;
    public GameObject soundMenu;
    public GameObject gameMenu;

    private GameObject currentMenu;
    private GameObject lastMenu;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        mainCanvas.worldCamera = Camera.main;
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            mainMenu.SetActive(true);
            currentMenu = mainMenu;
            currentMenu.SetActive(true);
        }
        else
        {
            currentMenu = gameMenu;
            mainMenu.SetActive(false);
        }
    }

    private void Update()
    {
        if (lastMenu && Input.GetKeyDown(KeyCode.Backspace))
        {
            currentMenu.SetActive(false);
            lastMenu.SetActive(true);
            currentMenu = lastMenu;
        }
        else if(SceneManager.GetActiveScene().buildIndex != 0 && Input.GetKeyDown(KeyCode.Backspace))
        {
            OnClickReturnToGame();
        }

        if (SceneManager.GetActiveScene().buildIndex != 0 && Input.GetKeyDown(KeyCode.Escape))
        {
            if (mainCanvas.gameObject.activeSelf)
            {
                OnClickReturnToGame();
            }
            else
            {
                mainCanvas.gameObject.SetActive(true);
                currentMenu.SetActive(true);
            }
        }
    }

    public void OnClickStartNewLevel()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);
        OnClickReturnToGame();
    }

    public void OnClickQuit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void OnClickOptions()
    {
        lastMenu = currentMenu;
        currentMenu.SetActive(false);
        soundMenu.SetActive(true);
        currentMenu = soundMenu;
    }

    public void OnClickReturnToGame()
    {
        currentMenu.SetActive(false);
        mainCanvas.gameObject.SetActive(false);
        currentMenu = gameMenu;
    }

    public void OnClickReturn()
    {
        currentMenu.SetActive(false);
        lastMenu.SetActive(true);
        currentMenu = lastMenu;
    }

    public void OnClickMainMenu()
    {
        SceneManager.LoadScene(sceneBuildIndex: 0);
        Destroy(gameObject);
    }
}
