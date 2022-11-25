using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
    public static bool gameIsPaused;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            transform.GetChild(4).gameObject.SetActive(false);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Continue()
    {
        gameIsPaused = !gameIsPaused;
        PauseGame();
        Debug.Log("sacar pausa");
    }
}
