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

    public static void UpPressed()
    {
        if (OnUpPressed != null)
            OnUpPressed();
    }
    public static void DownPressed()
    {
        if (OnDownPressed != null)
            OnDownPressed();
    }
    public static void LeftPressed()
    {
        if (OnLeftPressed != null)
            OnLeftPressed();
    }
    public static void RightPressed()
    {
        if (OnRightPressed != null)
            OnRightPressed();
    }
    public static void ChangeViewPressed()
    {
        if (OnChangeViewPressed != null)
            OnChangeViewPressed();
    }
}
