using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintButton : MonoBehaviour
{
    
    private string howToUseText = "How to use";

    private Button myButton;
    private Text myButtonText;
    
    private void Awake()
    {
        myButton = gameObject.GetComponent<Button>();
        myButtonText = gameObject.GetComponent<Text>();
        myButton.onClick.AddListener(HintButtonPressed);
    }

    private void HintButtonPressed()
    {
        //If player has hints left
        if (GameManager.Instance.hintsLeft > 0 && GameManager.Instance.hintActive)
        {
            ActivateCameraHintSystem();
            GameManager.Instance.HintUsed();
            myButtonText.text = howToUseText;
        }
        else
        {
            ShowAdMenu();
        }

        if (GameManager.Instance.hintActive)
        {
            //if hint is already active and player presses button again, show tutorial on how to use hint
            //or hint is used first time
        }

    }
    private void ActivateCameraHintSystem()
    {

    }
    private void ShowAdMenu()
    {

    }

}
