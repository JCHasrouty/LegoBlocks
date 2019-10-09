using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public RectTransform panelSimulationPauseMenu;
    public static bool GameIsPaused = true;
    public Button resumeButton;

    // Start is called before the first frame update
    void Start()
    {
        Button resButton = resumeButton.GetComponent<Button>();
        resButton.onClick.AddListener(PauseGame);
    }

    void PauseGame()
    {
        if(GameIsPaused)
        {
            Time.timeScale = 0f;
            GameIsPaused = false;
        }
        else
        {
            GameIsPaused = true;
            Time.timeScale = 1f;
            panelSimulationPauseMenu.transform.gameObject.SetActive(false);
        }
    }

    
}
