using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class MobileControllers : MonoBehaviour
{
    public Button changeView, up, down, left, right, back, restartButton;

    public float moveHorizontal = 0, moveVertical;
    public bool canPress = true;

    private CameraMovement cameraMovement;

    private void OnEnable()
    {
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        //SceneManager.sceneLoaded += OnSceneLoaded;
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

    private void Initialize()
    {
        changeView.onClick.AddListener(() => ViewChange());
        back.onClick.AddListener(() => GoToMenu());
        restartButton.onClick.AddListener(() => RestartCurrentLevel());
        restartButton.gameObject.SetActive(false);

        EventTrigger upTrigger = up.GetComponent<EventTrigger>();
        EventTrigger.Entry upEntry = new EventTrigger.Entry();
        upEntry.eventID = EventTriggerType.PointerDown;
        upEntry.callback.AddListener((data) => { GoUp(); });
        upTrigger.triggers.Add(upEntry);

        EventTrigger downTrigger = down.GetComponent<EventTrigger>();
        EventTrigger.Entry downEntry = new EventTrigger.Entry();
        downEntry.eventID = EventTriggerType.PointerDown;
        downEntry.callback.AddListener((data) => { GoDown(); });
        downTrigger.triggers.Add(downEntry);

        EventTrigger leftTrigger = left.GetComponent<EventTrigger>();
        EventTrigger.Entry leftEntry = new EventTrigger.Entry();
        leftEntry.eventID = EventTriggerType.PointerDown;
        leftEntry.callback.AddListener((data) => { GoLeft(); });
        leftTrigger.triggers.Add(leftEntry);

        EventTrigger rightTrigger = right.GetComponent<EventTrigger>();
        EventTrigger.Entry rightEntry = new EventTrigger.Entry();
        rightEntry.eventID = EventTriggerType.PointerDown;
        rightEntry.callback.AddListener((data) => { GoRight(); });
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
    public void GoUp()
    {
        if (canPress)
            moveVertical = 1;
    }
    public void GoDown()
    {
        if (canPress)
            moveVertical = -1;
    }
    public void GoLeft()
    {
        if (canPress)
            moveHorizontal = -1;
    }
    public void GoRight()
    {
        if (canPress)
            moveHorizontal = 1;
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
