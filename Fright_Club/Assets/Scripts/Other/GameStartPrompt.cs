using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStartPrompt : MonoBehaviour
{
    public static bool GameStartPromptIsActive = false;
    public GameObject GameStart_Prompt;
    public GameObject OBJ_BTN_1;
    public GameObject OBJ_BTN_2;
    public GameObject OBJ_BTN_3;
    public GameObject OBJ_BTN_4;
    public GameObject OBJ_BTN_5;
    public GameObject OBJ_BTN_6;
    public GameObject OBJ_BTN_7;
    public GameObject OBJ_BTN_8;
    public GameObject OBJ_BTN_9;
    public GameObject OBJ_BTN_10;
    public GameObject OBJ_BTN_11;


    // Start is called before the first frame update
    void Start()
    {
        Scene CurrentScene = SceneManager.GetActiveScene();
        string CurrentSceneName = CurrentScene.name;

        if (CurrentSceneName == "GameLevel")
        {
            ActivateGameStartPrompt();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ActivateGameStartPrompt()
    {
        Time.timeScale = 0f;
        GameStartPromptIsActive = true;
        GameStart_Prompt.SetActive(true);
        OBJ_BTN_1.SetActive(true);
    }

    public void BTN_1()
    {
        OBJ_BTN_1.SetActive(false);
        OBJ_BTN_2.SetActive(true);
    }

    public void BTN_2()
    {
        OBJ_BTN_2.SetActive(false);
        OBJ_BTN_3.SetActive(true);
    }

    public void BTN_3()
    {
        OBJ_BTN_3.SetActive(false);
        OBJ_BTN_4.SetActive(true);
    }

    public void BTN_4()
    {
        OBJ_BTN_4.SetActive(false);
        OBJ_BTN_5.SetActive(true);
    }

    public void BTN_5()
    {
        OBJ_BTN_5.SetActive(false);
        OBJ_BTN_6.SetActive(true);
    }

    public void BTN_6()
    {
        OBJ_BTN_6.SetActive(false);
        OBJ_BTN_7.SetActive(true);
    }

    public void BTN_7()
    {
        OBJ_BTN_7.SetActive(false);
        OBJ_BTN_8.SetActive(true);
    }

    public void BTN_8()
    {
        OBJ_BTN_8.SetActive(false);
        OBJ_BTN_9.SetActive(true);
    }

    public void BTN_9()
    {
        OBJ_BTN_9.SetActive(false);
        OBJ_BTN_10.SetActive(true);
    }

    public void BTN_10()
    {
        OBJ_BTN_10.SetActive(false);
        OBJ_BTN_11.SetActive(true);
    }

    public void BTN_11()
    {
        GameStart_Prompt.SetActive(false);
        GameStartPromptIsActive = false;
        Time.timeScale = 1f;
        OBJ_BTN_11.SetActive(false); 
    }
}
