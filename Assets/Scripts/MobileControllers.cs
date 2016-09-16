using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class MobileControllers : MonoBehaviour
{

    private Button ChangeView;
    private Button Up, Down, Left, Right;
    private Button Back;
    public static float moveHorizontal = 0, moveVertical;
    // Use this for initialization
    void Start()
    {
        ChangeView = GameObject.Find("ChangeView").GetComponent<Button>();
        Up = GameObject.Find("Up").GetComponent<Button>();
        Down = GameObject.Find("Down").GetComponent<Button>();
        Left = GameObject.Find("Left").GetComponent<Button>();
        Right = GameObject.Find("Right").GetComponent<Button>();
        Back = GameObject.Find("Back").GetComponent<Button>();
        Initial();
    }

    // Update is called once per frame
    void Initial()
    {
        ChangeView.onClick.AddListener(() => ViewChange());
        Back.onClick.AddListener(() => GoToMenu());

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
        CameraMovement.changeMade = true;
    }
    public void GoUp()
    {
        moveVertical = 1;
    }
    public void GoDown()
    {
        moveVertical = -1;
    }
    public void GoLeft()
    {
        moveHorizontal = -1;
    }
    public void GoRight()
    {
        moveHorizontal = 1;
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    
}
