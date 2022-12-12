using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseControl : MonoBehaviour
{
    public GameObject Crosshair;

    public static bool gameIsPaused;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
    }

    void PauseGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.Confined;
            transform.GetChild(4).gameObject.SetActive(true);
            Crosshair.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            transform.GetChild(4).gameObject.SetActive(false);
            Crosshair.SetActive(true);
        }
    }

    public void Continue()
    {
        gameIsPaused = !gameIsPaused;
        PauseGame();
    }

    public void Restart()
    {
        gameIsPaused = !gameIsPaused;
        PauseGame();
        SceneManager.LoadScene("StartMenu");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
