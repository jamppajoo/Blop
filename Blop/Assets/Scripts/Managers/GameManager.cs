using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

/*
    This Gameobject is singleton, so it's always running on background.
    Handles storing all non permanent data
    Handles going to different levels and/or scenes
*/

public class GameManager : MonoBehaviour
{
    #region Singleton

    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    #endregion

    [Header("Bunch of data that will be overwritten by SaveAndLoad.cs and/or game scripts")]
    public int[] levelsStarAmount;
    public int[] levelPlayedAmount;
    public uint totalGameTime;
    public uint timeSinceGameOpened;

    public string levelName;
    public int levelNumber;
    public int levelPackNumber;

    public bool hintActive = false;
    public string[] LevelPackNames;
    private int totalStarAmount;
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
        SaveAndLoad.Instance.Load();
        timeSinceGameOpened = (uint)Time.time;
        totalGameTime = SaveAndLoad.Instance.loadedGameTime + timeSinceGameOpened;
        Application.targetFrameRate =500;
    }
    private void Update()
    {
        timeSinceGameOpened = (uint)Time.time;
        totalGameTime = SaveAndLoad.Instance.loadedGameTime + timeSinceGameOpened;
    }
    public int ReturnTotalStarAmount()
    {
        totalStarAmount = 0;

        for (int i = 0; i < levelsStarAmount.Length; i++)
        {
            if (levelsStarAmount[i] != 4)
                totalStarAmount += levelsStarAmount[i];
        }
        return totalStarAmount;
    }

    public void LoadMenu()
    {
        hintActive = false;
        SceneManager.LoadScene("Menu");
        AnalyticsManager.Instance.RestartValues();
    }
    public void RestartScene()
    {
        FindObjectOfType<LevelsManager>().RestartLevel();
        AnalyticsManager.Instance.AddRestartAmount();
    }
    public void LoadLevel(string levelName, bool fromMenu)
    {
        this.levelName = levelName;
        string[] currentLevelText = levelName.Split('.');
        levelNumber = int.Parse(currentLevelText[1]);
        levelPackNumber = int.Parse(currentLevelText[0].Replace("Level", ""));
        if (fromMenu)
        {
            SceneManager.LoadScene(levelPackNumber);
        }
        else
        {
            FindObjectOfType<LevelsManager>().LoadLevel(levelName);
            EventManager.LevelLoaded();
        }
        AnalyticsManager.Instance.RestartValues();
        AnalyticsManager.Instance.SetLevelName(levelName);
        AnalyticsManager.Instance.SendLevelStartedAnalytics();
    }
    public void HintUsed()
    {
        SaveAndLoad.Instance.Save();
        hintActive = true;
    }
    private void OnApplicationQuit()
    {
        AnalyticsManager.Instance.SendRageQuitAnalytics();
    }

    public void SmallVibrate()
    {
        if (Application.isEditor)
            return;
#if UNITY_ANDROID
        if (SettingsManager.Instance.usingVibrate)
            Vibration.SmallVibrate();
#endif
    }
}

