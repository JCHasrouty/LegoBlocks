using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public RectTransform panelSimulationPauseMenu;
    //public static bool GameIsPaused = true;
    public bool GameIsPaused;
    public Button resumeButton;

    // Start is called before the first frame update
    void Start()
    {
        GameIsPaused = false;    
    }
    void Update()
    {
        Button resButton = resumeButton.GetComponent<Button>();
        if (!GameIsPaused)
        {
            resButton.onClick.AddListener(PauseGame);
        }
        else
        {
            resButton.onClick.AddListener(UnPause);
        }
    }

    //void PauseGame()
    //{
    //    if(GameIsPaused)
    //    {
    //        Time.timeScale = 0f;
    //        GameIsPaused = false;
    //    }
    //    else
    //    {
    //        GameIsPaused = true;
    //        Time.timeScale = 1f;
    //        panelSimulationPauseMenu.transform.gameObject.SetActive(false);
    //    }
    //}

    void PauseGame()
    {
        GameIsPaused = true;
        Time.timeScale = 0f;
        panelSimulationPauseMenu.transform.gameObject.SetActive(true);
    }
    void UnPause()
    {
        GameIsPaused = false;
        Time.timeScale = 1f;
        panelSimulationPauseMenu.transform.gameObject.SetActive(false);
    }

    
}
