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
        EventManager.OnUpPressed += OnUpPressed;
        EventManager.OnDownPressed += OnDownPressed;
        EventManager.OnLeftPressed += OnLeftPressed;
        EventManager.OnRightPressed += OnRightPressed;
        EventManager.OnChangeViewPressed += OnChangeViewPressed;
        //EventManager.OnButtonPressed += ButtonPressed;
    }

    private void ButtonPressed()
    {
        myText.text = BlopMovement.buttonPresses.ToString();
    }

    private void OnDisable()
    {
        //EventManager.OnButtonPressed -= ButtonPressed;
        EventManager.OnUpPressed -= OnUpPressed;
        EventManager.OnDownPressed -= OnDownPressed;
        EventManager.OnLeftPressed -= OnLeftPressed;
        EventManager.OnRightPressed -= OnRightPressed;
        EventManager.OnChangeViewPressed -= OnChangeViewPressed;
    }

    private void OnChangeViewPressed()
    {
        myText.text = BlopMovement.buttonPresses.ToString();
    }

    private void OnUpPressed()
    {
        myText.text = BlopMovement.buttonPresses.ToString();
    }

    private void OnDownPressed()
    {
        myText.text = BlopMovement.buttonPresses.ToString();
    }

    private void OnLeftPressed()
    {
        myText.text = BlopMovement.buttonPresses.ToString();
    }

    private void OnRightPressed()
    {
        myText.text = BlopMovement.buttonPresses.ToString();
    }
}
