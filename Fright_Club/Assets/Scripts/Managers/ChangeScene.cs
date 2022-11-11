using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

    //Change scene based on scene number in build settings
    public void LoadScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    //Closes application
    public void ExitGame()
    {
        Application.Quit();
    }

    //Resumes game after closing pause menu
    public void ResumeGame()
    {
        if (pauseMenu)
        {
            pauseMenu.SetActive(false);
            //unfreezes gameplay
            Time.timeScale = 1;
        }
    }

    //shows pause menu when pressing escape
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu)
            {
                pauseMenu.SetActive(true);
                //anything happening in the game pauses when pause menu appears
                Time.timeScale = 0;
                Cursor.visible = true;
            }
        }
    }
}
