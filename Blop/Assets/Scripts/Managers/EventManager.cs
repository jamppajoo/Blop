using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
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
            ButtonPressed();
        }
    }
    public static void DownPressed()
    {
        if (OnDownPressed != null)
        {
            OnDownPressed();
            ButtonPressed();
        }
    }
    public static void LeftPressed()
    {
        if (OnLeftPressed != null)
        {
            OnLeftPressed();
            ButtonPressed();
        }
    }
    public static void RightPressed()
    {
        if (OnRightPressed != null)
        {
            OnRightPressed();
            ButtonPressed();
        }
    }
    public static void ChangeViewPressed()
    {
        if (OnChangeViewPressed != null)
        {
            OnChangeViewPressed();
            ButtonPressed();
        }
    }
    public static void ButtonPressed()
    {
        if (OnButtonPressed != null)
            OnButtonPressed();
    }
}
