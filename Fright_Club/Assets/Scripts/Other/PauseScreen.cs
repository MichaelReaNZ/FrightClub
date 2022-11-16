using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static GameManager;
using static GameStartPrompt;

public class PauseScreen : MonoBehaviour
{

    public static bool GameIsActive = true;
    public GameObject PauseScreenUI;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameStartPromptIsActive == false)
        {
            transform.Translate(Vector3.down * Time.deltaTime);
            if (Input.GetKeyDown("escape") && GameIsActive == true)
            {
                PauseGame();
            }
            else if (Input.GetKeyDown("escape") && GameIsActive == false)
            {
                ResumeGame();
            }
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        PauseScreenUI.SetActive(true);
        GameIsActive = false;
        AudioListener.pause = true;   
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        PauseScreenUI.SetActive(false);
        GameIsActive = true;
        AudioListener.pause = false;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        if (AudioListener.pause == true)
        {
            AudioListener.pause = false;
        }
    }

    public void QuitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
        Application.Quit();
    }

    // AudioSource.ignoreListenerPause=true; 
    // ^ Continues music in pause state,
    // useful for music in pause menu.
}
