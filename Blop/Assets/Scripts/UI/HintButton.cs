using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintButton : MonoBehaviour
{

    private string howToUseText = "How to \n use";

    private Button myButton;
    private Text myButtonText;

    private HintSystem hintSystem;

    private void Awake()
    {
        myButton = gameObject.GetComponent<Button>();
        myButtonText = gameObject.GetComponentInChildren<Text>();
        myButton.onClick.AddListener(HintButtonPressed);

        hintSystem = FindObjectOfType<HintSystem>();
    }
    private void Start()
    {
        myButtonText.text += ": " + GameManager.Instance.hintsLeft.ToString();
    }
    private void HintButtonPressed()
    {
        //if hint is already active and player presses button again, show tutorial on how to use hint
        if (GameManager.Instance.hintActive)
        {
            hintSystem.ShowMenu(true);
        }
        //If player has hints left
        if (GameManager.Instance.hintsLeft > 0 && !GameManager.Instance.hintActive)
        {
            GameManager.Instance.HintUsed();
            myButtonText.text = howToUseText;
        }
        else
        {
            ShowAdMenu();
        }


    }
    private void ShowAdMenu()
    {

    }

}
