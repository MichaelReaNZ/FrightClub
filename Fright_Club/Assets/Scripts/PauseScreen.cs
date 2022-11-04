using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static GameManager;

public class PauseScreen : MonoBehaviour
{

    public static bool GameIsActive = true;
    public GameObject OBJ_PauseScreen;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        GameObject OBJ_PauseScreen = GameObject.Find("OBJ_PauseScreen");
        OBJ_PauseScreen.gameObject.SetActive(false);
        transform.Translate(Vector3.down * Time.deltaTime);
        if (Input.GetKeyDown("space"))
        {
            PauseGame();

            if (GameIsActive == false)
            {
                if (Input.GetKeyDown("space"))
                {
                    ResumeGame();
                }
            }
        }

    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        GameIsActive = false;
        GameObject OBJ_PauseScreen = GameObject.Find("OBJ_PauseScreen");
        OBJ_PauseScreen.gameObject.SetActive(true);
        AudioListener.pause = true;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        GameIsActive = true;
        GameObject OBJ_PauseScreen = GameObject.Find("OBJ_PauseScreen");
        OBJ_PauseScreen.gameObject.SetActive(false);
        AudioListener.pause = false;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
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
