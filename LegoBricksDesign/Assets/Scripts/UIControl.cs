﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    //// only way to reference the panel
    //public RectTransform panelMainMenu;
    //public RectTransform panelSetting;

    public InputField inpXDim;
    public InputField inpYDim;
    
    public Toggle togglePhysics;
    string LegoBlockSpeed = "";
    string LegoGameMode = "";

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
        bool physicsStatus = Convert.ToBoolean(togglePhysics.isOn);

        PlayerPrefs.SetString("X", x.ToString());
        PlayerPrefs.SetString("Y", y.ToString());
        PlayerPrefs.SetString("TogglePhysics", physicsStatus.ToString());

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
