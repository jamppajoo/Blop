using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/*
    Main function of this scipt is to find every component relative to LevelPassedPanel
    and make it work (stars, buttons ect). Also displays how many times any button has
    been pressed to the screen. But that's only for dev purposes.
*/

public class LevelStarSystem : MonoBehaviour
{

    public int threeStarMovementAmount, twoStarMovementAmount;
    public Button nextLevel, restartLevel;
    public GameObject oneStar, twoStar, threeStar, levelPassedPanel;
    private int buttonPressesAmount = 0;
    [HideInInspector]
    public int stars;
    private MobileControllers mobileControllers;
    private Finish finish;

    private void Awake()
    {
        finish = FindObjectOfType<Finish>();
        stars = 3;
        mobileControllers = FindObjectOfType<MobileControllers>();
        //Set levelPassedPanel not active since we just started level
        nextLevel.GetComponent<Button>().onClick.AddListener(() => finish.NextLevel());
        restartLevel.GetComponent<Button>().onClick.AddListener(() => Restart());
    }
    private void Start()
    {
        levelPassedPanel.SetActive(false);
    }
    private void Update()
    {
        //Check how many times any button has been pressed since level started
        buttonPressesAmount = BlobMovement.buttonPresses;

        stars = 1;
        if (buttonPressesAmount > threeStarMovementAmount && buttonPressesAmount <= twoStarMovementAmount)
            stars = 2;
        else if (buttonPressesAmount <= threeStarMovementAmount)
            stars = 3;

        LevelPassed(stars);

    }
    
    public void LevelPassed(int stars)
    {
        switch (stars)
        {
            case 1:
                twoStar.SetActive(false);
                nextLevel.gameObject.SetActive(true);
                break;
            case 2:
                threeStar.SetActive(false);
                nextLevel.gameObject.SetActive(true);
                break;
            case 3:
                nextLevel.gameObject.SetActive(true);
                break;
        }
    }

    public void ShowLevelPassedScreen()
    {
        levelPassedPanel.SetActive(true);
        mobileControllers.gameObject.SetActive(false);

    }

    //Restart button on levelpassPanel
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
