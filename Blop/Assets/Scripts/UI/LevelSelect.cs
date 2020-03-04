using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{

    private LevelSelectButton[] levelButtons;

    private void Awake()
    {
        levelButtons = gameObject.GetComponentsInChildren<LevelSelectButton>();   
    }
    private void Start()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if((levelButtons[i].myLevelNumber* levelButtons[i].myLevelpackNumber)-1 == i)
            {
                levelButtons[i].SetStarAmount(GameManager.Instance.levelsStarAmount[i]);
            }
        }
    }
    public void StartScene(string levelName)
    {
        GameManager.Instance.LoadLevel(levelName, true);
    }

}
