using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    private Level[] levels;

    private BlopMovement playerMovement;
    private CameraMovement cameraMovement;
    private ButtonPressesText buttonPressesText;
    private GameObject currentLevel;

    private void Awake()
    {
        levels = gameObject.GetComponentsInChildren<Level>();

        playerMovement = FindObjectOfType<BlopMovement>();
        cameraMovement = FindObjectOfType<CameraMovement>();
        buttonPressesText = FindObjectOfType<ButtonPressesText>();

    }
    private void Start()
    {
        GameManager.Instance.LoadLevel(GameManager.Instance.levelName, false);
    }

    public void LoadLevel(string levelName)
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (levels[i].levelName == levelName)
            {
                levels[i].LoadLevel();
                currentLevel = levels[i].gameObject;
                RestartLevel();
            }
            else
                levels[i].UnLoadLevel();
        }
    }
    public void RestartLevel()
    {
        playerMovement.RestartPlayer(currentLevel.transform.localPosition);
        cameraMovement.RestartCamera();
        buttonPressesText.RestartButtonPresses();
    }
}
