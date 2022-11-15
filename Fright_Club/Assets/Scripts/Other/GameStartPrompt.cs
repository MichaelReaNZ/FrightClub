using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameStartPrompt : MonoBehaviour
{
    public static bool GameStartPromptIsActive = false;
    public GameObject GameStart_Prompt;
    public TextMeshProUGUI Text1;
    public GameObject OBJ_BTN;
    public GameObject OBJ_BTN_1;
    public GameObject OBJ_BTN_2;
    public GameObject OBJ_BTN_3;
    public GameObject OBJ_BTN_4;
    public GameObject OBJ_BTN_5;
 


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
        Text1.gameObject.SetActive(true);
        OBJ_BTN.SetActive(true);
    }

    public void BTN()
    {
        OBJ_BTN.SetActive(false);
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
        Text1.gameObject.SetActive(false);
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
        GameStart_Prompt.SetActive(false);
        GameStartPromptIsActive = false;
        Time.timeScale = 1f;
    }
}