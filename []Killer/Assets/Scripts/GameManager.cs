using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    #endregion

    private bool m_gameIsPaused = false;
    public bool gameIsPaused { get { return m_gameIsPaused; } set { m_gameIsPaused = value; } }

    public Transform playerSpawn;

    public GameObject pauseMenuUI;
    public GameObject optionMenuUI;
    public GameObject deathPanel;

    private Player playerInstance;

    void Start()
    {
        gameIsPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerInstance = Player.instance;
    }

    void Update()
    {
        if (playerInstance.isAlive && deathPanel.activeSelf)
            deathPanel.SetActive(false);
        else if(!playerInstance.isAlive && !deathPanel.activeSelf)
            deathPanel.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        optionMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        gameIsPaused = true;
    }

    public void LoadLevel(int sceneIndex)
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        SceneManager.LoadScene(sceneIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
