using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Canvas mainCanvas;
    public GameObject mainMenu;
    public GameObject soundMenu;
    public GameObject gameMenu;
    public GameObject returnToGame;

    private GameObject currentMenu;
    private GameObject lastMenu;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        mainCanvas.worldCamera = Camera.main;
        mainCanvas.planeDistance = 0.5f;
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
                Time.timeScale = 0;
                mainCanvas.gameObject.SetActive(true);
                currentMenu.SetActive(true);
                EventSystem.current.SetSelectedGameObject(returnToGame);
            }
        }
    }

    public void OnClickStartNewLevel()
    {
        SceneManager.LoadScene(1);
        OnClickReturnToGame();
    }

    public void OnClickQuit()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
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
        Time.timeScale = 1;
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
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
