using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        Up.onClick.AddListener(() => GoUp());
        Down.onClick.AddListener(() => GoDown());
        Left.onClick.AddListener(() => GoLeft());
        Right.onClick.AddListener(() => GoRight());
        Back.onClick.AddListener(() => GoToMenu());

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
