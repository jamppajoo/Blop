using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
    Handles stars and saves them to the file when Save() function is called. When Load() function is called, it loads information from file.
    This Gameobject is singleton, so it's always running on background.
    Everything in GameManager has to be in specific order in Unity becouse of GetChild() methods.
*/

public class GameManager : Singleton<GameManager>
{
    public int[] LevelPack1Stars;
    public int[] LevelPack2Stars;
    public int[] LevelPack3Stars;

    private int totalStarAmount;
    
    void Awake()
    {
        SaveAndLoad.Instance.Load();
    }

    public int ReturnTotalStarAmount()
    {
        totalStarAmount = 0;

        for (int i = 0; i < LevelPack1Stars.Length; i++)
        {
            if (LevelPack1Stars[i] != 4)
                totalStarAmount += LevelPack1Stars[i];
        }
        for (int i = 0; i < LevelPack2Stars.Length; i++)
        {
            if (LevelPack2Stars[i] != 4)
                totalStarAmount += LevelPack2Stars[i];
        }
        for (int i = 0; i < LevelPack3Stars.Length; i++)
        {
            if (LevelPack3Stars[i] != 4)
                totalStarAmount += LevelPack3Stars[i];
        }
        return totalStarAmount;
    }
    
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

