using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles storing in-game settings
/// </summary>
public class SettingsManager : MonoBehaviour
{
    #region Singleton

    private static SettingsManager _instance;

    public static SettingsManager Instance { get { return _instance; } }

    #endregion

    public bool usingVibrate = true;

    private void Awake()
    {
        #region Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
        #endregion
    }
    

}
