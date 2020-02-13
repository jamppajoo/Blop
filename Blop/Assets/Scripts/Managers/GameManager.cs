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

    public int hintsLeft = 3;
    public bool hintActive = false;
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
    private void OnEnable()
    {
        SceneManager.sceneLoaded += NewSceneLoaded;
    }

    private void NewSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        levelName = SceneManager.GetActiveScene().name;
    }

    private int totalStarAmount;

    private void Start()
    {
        SaveAndLoad.Instance.Load();
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
    }
    public void HintUsed()
    {
        hintsLeft--;
        SaveAndLoad.Instance.Save();
        hintActive = true;
    }
    public void AddHint()
    {
        hintsLeft++;
        SaveAndLoad.Instance.Save();
    }
}

