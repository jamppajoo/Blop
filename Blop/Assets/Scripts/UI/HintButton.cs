﻿using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handle hint button functionality, showing ad menu and how to use hint menu
/// </summary>
public class HintButton : MonoBehaviour
{
    private string howToUseText = "How to \n use";
    private string originalText;

    private Button myButton;
    private Text myButtonText;

    private HintSystem hintSystem;
    private AdController adController;

    private void OnEnable()
    {
        EventManager.OnWatchedAd += WatchedAd;
        EventManager.OnNewLevelLoaded += NewLevelLoaded;
    }
    private void OnDisable()
    {
        EventManager.OnWatchedAd -= WatchedAd;
        EventManager.OnNewLevelLoaded -= NewLevelLoaded;
    }

    private void NewLevelLoaded()
    {
        myButtonText.text = originalText;
    }

    private void WatchedAd()
    {
        myButtonText.text = howToUseText;
    }

    private void Awake()
    {
        myButton = gameObject.GetComponent<Button>();
        myButtonText = gameObject.GetComponentInChildren<Text>();
        myButton.onClick.AddListener(HintButtonPressed);

        originalText = myButtonText.text;
        hintSystem = FindObjectOfType<HintSystem>();
        adController = FindObjectOfType<AdController>();
    }
    private void Start()
    {
        if (GameManager.Instance.hintActive)
            myButtonText.text = howToUseText;
    }
    private void HintButtonPressed()
    {
        GameManager.Instance.SmallVibrate();
        //if hint is already active and player presses button again, show tutorial on how to use hint
        if (GameManager.Instance.hintActive)
        {
            hintSystem.ShowMenu(true);
        }
        else
        {
            ShowAdMenu();
        }


    }
    private void ShowAdMenu()
    {
        adController.ShowMenu(true);
    }

}
