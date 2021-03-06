﻿using UnityEngine.SceneManagement;
using UnityEngine;

/**
 * Author:          Sebastian Przyszlak
 * Collaborators:   Anna Mach - save, loadData
 */
public class PauseManager : MonoBehaviour {
    #region Singleton
    public static PauseManager instance;

    void Awake () {
        if (instance == null)
            instance = this;
        else {
            Destroy (gameObject);
            return;
        }
    }
    #endregion

    private void Start () {
        gameIsPaused = false;
    }

    private void Update () {
        if (Input.GetKeyDown (KeyCode.Escape)) {
            if (gameIsPaused)
                Resume ();
            else
                Pause ();
        }
    }

    public static bool gameIsPaused;
    public GameObject pauseMenuUI;
    public GameObject optionMenuUI;
    public GameObject saveLoadThings;

    public void Resume () {
        PlayerManager.instance.player.GetComponent<Player> ()?.InputEnabled (true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameIsPaused = false;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive (false);
        optionMenuUI.SetActive (false);
    }

    public void SaveLevel()
    {
        saveLoadThings.GetComponent<DataToSaveLoad>().RefreshStoredData();
        saveLoadThings.GetComponent<SaveLoad>().Save();
    }

    public void LoadData()
    {
        saveLoadThings.GetComponent<SaveLoad>().Load();
    }

    void Pause () {
        PlayerManager.instance.player.GetComponent<Player> ()?.InputEnabled (false);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        gameIsPaused = true;
        Time.timeScale = 0f;
        pauseMenuUI.SetActive (true);
    }

    public void ResetLevel(int sceneIndex)
    {
        EnemyMom.shouldBeDead = false;
        LoadLevel(sceneIndex);
    }
    
    public void LoadLevel (int sceneIndex) {
        Time.timeScale = 1f;
        gameIsPaused = false;
        GameManager.inBattle = false;
        SceneManager.LoadScene (sceneIndex);
    }

    public void QuitGame () {
        Debug.Log ("Quit");
        Application.Quit ();
    }
}
