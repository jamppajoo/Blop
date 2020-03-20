﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class MobileControllers : MonoBehaviour
{
    public Button changeView, up, down, left, right, back, restartButton;

    public bool canPress = true;

    private CameraMovement cameraMovement;

    private void OnEnable()
    {
        EventManager.OnDisableIngameButtons += DisableButtons;
        EventManager.OnEnableIngameButtons += EnableButtons;
    }
    private void OnDisable()
    {
        EventManager.OnDisableIngameButtons -= DisableButtons;
        EventManager.OnEnableIngameButtons -= EnableButtons;
    }
    
    private void EnableButtons()
    {
        SetButtonsActive(true);
    }

    private void DisableButtons()
    {
        SetButtonsActive(false);
    }

    private void SetButtonsActive(bool canBePressed)
    {
        canPress = canBePressed;
        changeView.interactable = canBePressed;
        up.interactable = canBePressed;
        down.interactable = canBePressed;
        left.interactable = canBePressed;
        right.interactable = canBePressed;
    }
    
    private void Awake()
    {
        cameraMovement = FindObjectOfType<CameraMovement>();
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Vertical") > 0.5f)
            UpPressed();
        if (Input.GetAxisRaw("Vertical") < -0.5f)
            DownPressed();
        if (Input.GetAxisRaw("Horizontal") < -0.5f)
            LeftPressed();
        if (Input.GetAxisRaw("Horizontal") > 0.5f)
            RightPressed();

        if (Input.GetKeyDown(KeyCode.Space))
            ViewChange();
    }

    private void Initialize()
    {
        changeView.onClick.AddListener(() => ViewChange());
        back.onClick.AddListener(() => GoToMenu());
        restartButton.onClick.AddListener(() => RestartCurrentLevel());

        EventTrigger upTrigger = up.GetComponent<EventTrigger>();
        EventTrigger.Entry upEntry = new EventTrigger.Entry();
        upEntry.eventID = EventTriggerType.PointerDown;
        upEntry.callback.AddListener((data) => { UpPressed(); });
        upTrigger.triggers.Add(upEntry);

        EventTrigger downTrigger = down.GetComponent<EventTrigger>();
        EventTrigger.Entry downEntry = new EventTrigger.Entry();
        downEntry.eventID = EventTriggerType.PointerDown;
        downEntry.callback.AddListener((data) => { DownPressed(); });
        downTrigger.triggers.Add(downEntry);

        EventTrigger leftTrigger = left.GetComponent<EventTrigger>();
        EventTrigger.Entry leftEntry = new EventTrigger.Entry();
        leftEntry.eventID = EventTriggerType.PointerDown;
        leftEntry.callback.AddListener((data) => { LeftPressed(); });
        leftTrigger.triggers.Add(leftEntry);

        EventTrigger rightTrigger = right.GetComponent<EventTrigger>();
        EventTrigger.Entry rightEntry = new EventTrigger.Entry();
        rightEntry.eventID = EventTriggerType.PointerDown;
        rightEntry.callback.AddListener((data) => { RightPressed(); });
        rightTrigger.triggers.Add(rightEntry);
    }

    public void OnPointerDownDelegate(PointerEventData data)
    {
        Debug.Log(data);
    }

    public void ViewChange()
    {
        EventManager.ChangeViewPressed();
        EventManager.ButtonPressed();

    }
    public void UpPressed()
    {
        if (canPress)
        {
            EventManager.UpPressed();
            EventManager.ButtonPressed();
        }
    }
    public void DownPressed()
    {
        if (canPress)
        {
            EventManager.DownPressed();
            EventManager.ButtonPressed();
        }
    }
    public void LeftPressed()
    {
        if (canPress)
        {
            EventManager.LeftPressed();
            EventManager.ButtonPressed();
        }
    }
    public void RightPressed()
    {
        if (canPress)
        {
            EventManager.RightPressed();
            EventManager.ButtonPressed();
        }
    }
    public void GoToMenu()
    {
        AnalyticsManager.Instance.SendRageQuitAnalytics();
        GameManager.Instance.LoadMenu();
    }
    public void RestartCurrentLevel()
    {
        GameManager.Instance.RestartScene();
    }

}
