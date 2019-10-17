using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIControl : MySingleton<UIControl>
{
    //// only way to reference the panel
    public RectTransform panelMainMenu;
    public RectTransform panelSetting;
    public RectTransform panelSimulationMainMenu;
    public RectTransform panelSimulationPauseMenu;
    public RawImage backgroundImage;
    //public Button mButton;

     
    // Input Field Variables //
    public InputField inpXDim;
    public InputField inpYDim;
    int DefaultX = 25;
    int DefaultY = 25;
    //public Button MButton;



    //public Toggle togglePhysics;
    string LegoBlockSpeed = "";
    string LegoGameMode = "";

    public void OnEnable()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;

        // Set default plate values in case user does not specify
        PlayerPrefs.SetString("X", DefaultX.ToString());
        PlayerPrefs.SetString("Y", DefaultY.ToString());

    }
    public void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }
    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    { 
        switch (arg0.name)
        {
            case "MainMenu":
                panelMainMenu.transform.gameObject.SetActive(true);
                panelSetting.transform.gameObject.SetActive(false);
                panelSimulationMainMenu.transform.gameObject.SetActive(false);
                panelSimulationPauseMenu.transform.gameObject.SetActive(false);

                break;
            case "MainLevel":
                panelMainMenu.transform.gameObject.SetActive(false);
                panelSetting.transform.gameObject.SetActive(false);
                panelSimulationMainMenu.transform.gameObject.SetActive(false);
                panelSimulationPauseMenu.transform.gameObject.SetActive(false);
                backgroundImage.transform.gameObject.SetActive(false);

                break;
            default:
                // do something else
                break;
        }

    }
    public void butStartClicked()
    {
        SceneManager.LoadScene("MainLevel");
        Debug.Log("Button start clicked");
    }
    public void butExitClicked()
    {
        Application.Quit();
        Debug.Log("Button exit clicked");
    }
    public void SetButtonClicked()
    {
        // no error checking but take it into consideration
        int x = Convert.ToInt32(inpXDim.text);
        int y = Convert.ToInt32(inpYDim.text);
        //bool physicsStatus = Convert.ToBoolean(togglePhysics.isOn);

        PlayerPrefs.SetString("X", x.ToString());
        PlayerPrefs.SetString("Y", y.ToString());
        //PlayerPrefs.SetString("TogglePhysics", physicsStatus.ToString());

        Debug.Log(string.Format("The new dimension of board will be {0}x{1}", x, y));
    }
    public void SlowButtonClicked()
    { 
        LegoBlockSpeed = "slow";
        PlayerPrefs.SetString("BlockSpeed", LegoBlockSpeed);

    }
    public void FastButtonClicked()
    {
        LegoBlockSpeed = "fast";
        PlayerPrefs.SetString("BlockSpeed", LegoBlockSpeed);
    }
    public void UniformButtonClicked()
    {
        LegoGameMode = "uniform";
        PlayerPrefs.SetString("GameMode", LegoGameMode);
    }
    public void BreakAwayButtonClicked()
    {
        LegoGameMode = "break-away";
        PlayerPrefs.SetString("GameMode", LegoGameMode);
    }

}
