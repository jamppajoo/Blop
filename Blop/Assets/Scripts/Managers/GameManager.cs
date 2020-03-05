using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

/*
    Handles stars and saves them to the file when Save() function is called. When Load() function is called, it loads information from file.
    This Gameobject is singleton, so it's always running on background.
    Everything in GameManager has to be in specific order in Unity becouse of GetChild() methods.
*/

public class GameManager : MonoBehaviour
{
    #region Singleton

    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    #endregion

    public int[] levelsStarAmount;

    public string levelName;
    public int levelNumber;

    public bool hintActive = false;
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
        Application.targetFrameRate = 60;
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
        if (fromMenu)
        {
            SceneManager.LoadScene(1);
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
}

