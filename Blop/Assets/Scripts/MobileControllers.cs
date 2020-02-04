using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class MobileControllers : MonoBehaviour
{

    public static Button ChangeView, Up, Down, Left, Right,Back, RestartButton;
    public static float moveHorizontal = 0, moveVertical;
    public static bool canPress = true;
    public static MobileControllers controller;
    // Use this for initialization
    void Start()
    {
        ChangeView = GameObject.Find("ChangeView").GetComponent<Button>();
        Up = GameObject.Find("Up").GetComponent<Button>();
        Down = GameObject.Find("Down").GetComponent<Button>();
        Left = GameObject.Find("Left").GetComponent<Button>();
        Right = GameObject.Find("Right").GetComponent<Button>();
        Back = GameObject.Find("Back").GetComponent<Button>();
        RestartButton = GameObject.Find("RestartButton").GetComponent<Button>();
        Initial();
    }

    // Update is called once per frame
    void Initial()
    {
        ChangeView.onClick.AddListener(() => ViewChange());
        Back.onClick.AddListener(() => GoToMenu());
        RestartButton.onClick.AddListener(() => RestartCurrentLevel());
        RestartButton.gameObject.SetActive(false);

        EventTrigger upTrigger = Up.GetComponent<EventTrigger>();
        EventTrigger.Entry upEntry = new EventTrigger.Entry();
        upEntry.eventID = EventTriggerType.PointerDown;
        upEntry.callback.AddListener((data) => { GoUp(); });
        upTrigger.triggers.Add(upEntry);

        EventTrigger downTrigger = Down.GetComponent<EventTrigger>();
        EventTrigger.Entry downEntry = new EventTrigger.Entry();
        downEntry.eventID = EventTriggerType.PointerDown;
        downEntry.callback.AddListener((data) => { GoDown(); });
        downTrigger.triggers.Add(downEntry);

        EventTrigger leftTrigger = Left.GetComponent<EventTrigger>();
        EventTrigger.Entry leftEntry = new EventTrigger.Entry();
        leftEntry.eventID = EventTriggerType.PointerDown;
        leftEntry.callback.AddListener((data) => { GoLeft(); });
        leftTrigger.triggers.Add(leftEntry);

        EventTrigger rightTrigger = Right.GetComponent<EventTrigger>();
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
        CameraMovement.Instance.viewChanged = true;
        
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
