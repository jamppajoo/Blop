using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/*
    Main function of this scipt is to find every component relative to LevelPassedPanel
    and make it work (stars, buttons ect). Also displays how many times any button has
    been pressed to the screen. But that's only for dev purposes.
*/

public class LevelStarSystem : MonoBehaviour {

    private Text buttonPressesText;
    public int threeStarMovementAmount, twoStarMovementAmount, oneStarMovementAmount;
    private GameObject oneStar, twoStar, threeStar, nextLevel, restartLevel, finish, mobileControllers;
    static GameObject levelPassedPanel;
    public int buttonPressesAmount = 0;
    public int stars;


    // Use this for initialization
    void Awake () {
        stars = 3;
        buttonPressesText = GameObject.Find("ButtonPressesText").GetComponent<Text>();
        oneStar = GameObject.Find("oneStar");
        twoStar = GameObject.Find("twoStar");
        threeStar = GameObject.Find("threeStar");
        nextLevel = GameObject.Find("NextLevel");
        restartLevel = GameObject.Find("RestartLevel");
        levelPassedPanel = GameObject.Find("LevelPassedPanel");
        finish = GameObject.Find("Finish");
        restartLevel = GameObject.Find("RestartLevel");
        mobileControllers = GameObject.Find("MobileControllers");
        //Set levelPassedPanel not active since we just started level
        levelPassedPanel.SetActive(false);
        nextLevel.GetComponent<Button>().onClick.AddListener(() => finish.GetComponent<Finish>().nextLevel());
        restartLevel.GetComponent<Button>().onClick.AddListener(() => Restart());
    }
	
	// Update is called once per frame
	void Update () {
        //Check how many times any button has been pressed since level started
        buttonPressesAmount = BlobMovement.buttonPresses;
        buttonPressesText.text = buttonPressesAmount.ToString();

        if (buttonPressesAmount > oneStarMovementAmount)
            stars = 0;
        else if (buttonPressesAmount > twoStarMovementAmount)
            stars = 1;
            
        else if (buttonPressesAmount > threeStarMovementAmount)
            stars = 2;
        else if (buttonPressesAmount <= threeStarMovementAmount)
            stars = 3;

        levelPassed(stars);

    }
    //Controlled from Finish script
    public void levelPassed(int stars)
    {
        switch(stars)
        {
            case 0:
                oneStar.SetActive(false);
                nextLevel.SetActive(false);
                break;
            case 1:
                twoStar.SetActive(false);
                nextLevel.SetActive(true);
                break;
            case 2:
                threeStar.SetActive(false);
                nextLevel.SetActive(true);
                break;
            case 3:
                nextLevel.SetActive(true);
                break;
        }
        
    }
    public void showLevelPassedScreen()
    {
        
        levelPassedPanel.SetActive(true);
        mobileControllers.SetActive(false);
        
    }
    //Restart button on levelpassPanel
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
