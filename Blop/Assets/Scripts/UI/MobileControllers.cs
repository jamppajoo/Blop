using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
        EventManager.OnDisableIngameButtons += DisableButtons;
        EventManager.OnEnableIngameButtons += EnableButtons;
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
        changeView.interactable = canBePressed;
        up.interactable = canBePressed;
        down.interactable = canBePressed;
        left.interactable = canBePressed;
        right.interactable = canBePressed;
    }
    

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Initialize();
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
    }

    private void Initialize()
    {
        changeView.onClick.AddListener(() => ViewChange());
        back.onClick.AddListener(() => GoToMenu());
        restartButton.onClick.AddListener(() => RestartCurrentLevel());
        restartButton.gameObject.SetActive(false);

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
        cameraMovement.viewChanged = true;

    }
    public void UpPressed()
    {
        if (canPress)
            EventManager.UpPressed();
    }
    public void DownPressed()
    {
        if (canPress)
            EventManager.DownPressed();
    }
    public void LeftPressed()
    {
        if (canPress)
            EventManager.LeftPressed();
    }
    public void RightPressed()
    {
        if (canPress)
            EventManager.RightPressed();
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void RestartCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
