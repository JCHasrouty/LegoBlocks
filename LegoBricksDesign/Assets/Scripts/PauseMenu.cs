using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    //public RectTransform panelSimulationPauseMenu;
    public RectTransform MainMenu;
    //public static bool GameIsPaused = true;
    public bool GameIsPaused;
    public Button resumeButton;
    public Button resetButton;
    public Button playButton;

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

    void PauseGame()
    {
        Button resetButtonRef = resetButton.GetComponent<Button>();
        Button playButtonRef = playButton.GetComponent<Button>();
        GameIsPaused = true;
        Time.timeScale = 0f;
        MainMenu.transform.gameObject.SetActive(true);
        resetButtonRef.transform.gameObject.SetActive(true);
        playButtonRef.transform.gameObject.SetActive(false);
    }
    void UnPause()
    {
        GameIsPaused = false;
        Time.timeScale = 1f;
        MainMenu.transform.gameObject.SetActive(false);
    }

    
}
