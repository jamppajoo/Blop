using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibration : MonoBehaviour
{
    private static readonly AndroidJavaObject Vibrator =
      new AndroidJavaClass("com.unity3d.player.UnityPlayer")// Get the Unity Player.
      .GetStatic<AndroidJavaObject>("currentActivity")// Get the Current Activity from the Unity Player.
      .Call<AndroidJavaObject>("getSystemService", "vibrator");// Then get the Vibration Service from the Current Activity.

    static Vibration()
    {
        // Trick Unity into giving the App vibration permission when it builds.
        // This check will always be false, but the compiler doesn't know that.
        //if (Application.isEditor) Handheld.Vibrate();
    }
    public static void SmallVibrate()
    {
        Vibrator.Call("vibrate", (long)20);
    }
    public static void Vibrate(long milliseconds)
    {
        Vibrator.Call("vibrate", milliseconds);
    }


    public static void Vibrate(long[] pattern, int repeat)
    {
        Vibrator.Call("vibrate", pattern, repeat);
    }
}
