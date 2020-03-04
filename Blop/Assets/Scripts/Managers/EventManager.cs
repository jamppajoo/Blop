using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    #region InputHandler
    public delegate void InputHandler();

    public static event InputHandler OnUpPressed;
    public static event InputHandler OnDownPressed;
    public static event InputHandler OnLeftPressed;
    public static event InputHandler OnRightPressed;

    public static event InputHandler OnChangeViewPressed;
    public static event InputHandler OnButtonPressed;


    public static void UpPressed()
    {
        if (OnUpPressed != null)
        {
            OnUpPressed();
            //ButtonPressed();
        }
    }
    public static void DownPressed()
    {
        if (OnDownPressed != null)
        {
            OnDownPressed();
            //ButtonPressed();
        }
    }
    public static void LeftPressed()
    {
        if (OnLeftPressed != null)
        {
            OnLeftPressed();
            //ButtonPressed();
        }
    }
    public static void RightPressed()
    {
        if (OnRightPressed != null)
        {
            OnRightPressed();
            //ButtonPressed();
        }
    }
    public static void ChangeViewPressed()
    {
        if (OnChangeViewPressed != null)
        {
            OnChangeViewPressed();
            //ButtonPressed();
        }
    }
    public static void ButtonPressed()
    {
        if (OnButtonPressed != null)
        {
            OnButtonPressed();
        }
    }
    #endregion

    #region GeneralEvent

    public delegate void GeneralEvent();

    public static event GeneralEvent OnDisableIngameButtons;
    public static event GeneralEvent OnEnableIngameButtons;
    public static event GeneralEvent OnWatchedAd;
    public static event GeneralEvent OnNewLevelLoaded;

    public static void DisableIngameButtons()
    {
        if (OnDisableIngameButtons != null)
        {
            OnDisableIngameButtons();
        }
    }
    public static void EnableIngameButtons()
    {
        if (OnEnableIngameButtons != null)
        {
            OnEnableIngameButtons();
        }
    }
    public static void WatchedAd()
    {
        if (OnWatchedAd != null)
        {
            OnWatchedAd();
        }
    }
    public static void LevelLoaded()
    {
        if (OnNewLevelLoaded!= null)
        {
            OnNewLevelLoaded();
        }
    }
    #endregion

}
