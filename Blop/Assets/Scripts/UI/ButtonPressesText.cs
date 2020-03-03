using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPressesText : MonoBehaviour
{
    private Text myText;
    private void Awake()
    {
        myText = gameObject.GetComponent<Text>();

    }

    private void Start()
    {
        myText.text = BlopMovement.buttonPresses.ToString();
        
    }

    private void OnEnable()
    {
        EventManager.OnButtonPressed += ButtonPressed;
    }

    private void OnDisable()
    {
        EventManager.OnButtonPressed -= ButtonPressed;
    }

    private void ButtonPressed()
    {
        myText.text = BlopMovement.buttonPresses.ToString();
    }

    public void RestartButtonPresses()
    {
        myText.text = BlopMovement.buttonPresses.ToString();
    }


   
}
