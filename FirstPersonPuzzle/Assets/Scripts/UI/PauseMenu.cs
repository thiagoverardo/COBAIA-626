using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public bool paused = false;
    public GameObject pauseMenu;
    private GameMaster gm;
    private GameObject player;

    void Start()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                Time.timeScale = 0;
                paused = true;
                Cursor.visible = true;
                pauseMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                UnpauseGame();
            }
        }
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        paused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}