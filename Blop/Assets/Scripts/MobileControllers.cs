using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MobileControllers : Singleton<MobileControllers>
{
    public Button ChangeView, Up, Down, Left, Right,Back, RestartButton;

    public float moveHorizontal = 0, moveVertical;
    public bool canPress = true;

    void Start()
    {
        Initial();
    }
    
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
