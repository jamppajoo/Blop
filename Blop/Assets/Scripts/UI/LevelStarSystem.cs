using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
    Main function of this scipt is to find every component relative to LevelPassedPanel
    and make it work (stars, buttons ect). Also displays how many times any button has
    been pressed to the screen. But that's only for dev purposes.
*/

public class LevelStarSystem : MonoBehaviour
{
    private int threeStarMovementAmount, twoStarMovementAmount;
    public Button nextLevel, restartLevel, menuButton;
    public GameObject oneStar, twoStar, threeStar, levelPassedPanel;
    private int buttonPressesAmount = 0;
    [HideInInspector]
    public int stars;
    private MobileControllers mobileControllers;
    private Finish finish;
    private LevelStats levelStats;

    private void Awake()
    {
        finish = FindObjectOfType<Finish>();
        stars = 3;
        mobileControllers = FindObjectOfType<MobileControllers>();
        nextLevel.GetComponent<Button>().onClick.AddListener(NextLevel);
        restartLevel.GetComponent<Button>().onClick.AddListener(Restart);
        menuButton.GetComponent<Button>().onClick.AddListener(LoadMenu);
    }
    private void OnEnable()
    {
        EventManager.OnNewLevelLoaded += NewLevelLoaded;
    }
    private void OnDisable()
    {
        EventManager.OnNewLevelLoaded -= NewLevelLoaded;
    }

    private void Start()
    {
        levelPassedPanel.SetActive(false);
        NewLevelLoaded();
    }
    private void NewLevelLoaded()
    {
        levelStats = Resources.Load<LevelStats>(GameManager.Instance.levelName);
        twoStarMovementAmount = levelStats.twoStarMovementAmount;
        threeStarMovementAmount = levelStats.threeStarMovementAmount;
    }
    
    private void Update()
    {
        //Check how many times any button has been pressed since level started
        buttonPressesAmount = BlopMovement.buttonPresses;

        stars = 1;
        if (buttonPressesAmount <= twoStarMovementAmount && (buttonPressesAmount > threeStarMovementAmount || GameManager.Instance.hintActive))
            stars = 2;
        else if (buttonPressesAmount <= threeStarMovementAmount  && !GameManager.Instance.hintActive)
            stars = 3;

        LevelPassed(stars);

    }
    
    public void LevelPassed(int stars)
    {
        switch (stars)
        {
            case 1:
                twoStar.SetActive(false);
                break;
            case 2:
                threeStar.SetActive(false);
                break;
            case 3:
                break;
        }
    }

    public void ShowLevelPassedScreen()
    {
        levelPassedPanel.SetActive(true);
        mobileControllers.gameObject.SetActive(false);
    }

    //Restart button on levelpassPanel
    private void Restart()
    {
        GameManager.Instance.SmallVibrate();
        mobileControllers.gameObject.SetActive(true);
        levelPassedPanel.SetActive(false);
        twoStar.SetActive(true);
        threeStar.SetActive(true);
        GameManager.Instance.LoadLevel(GameManager.Instance.levelName, false);
        //GameManager.Instance.RestartScene();
        //AnalyticsManager.Instance.RestartValues();
        //levelPassedPanel.SetActive(false);
        //mobileControllers.gameObject.SetActive(true);
    }
    private void NextLevel()
    {
        GameManager.Instance.SmallVibrate();
        mobileControllers.gameObject.SetActive(true);
        levelPassedPanel.SetActive(false);
        //If next level is pressed on the levelpassedpanel, load new scene
        string[] currentLevelText = GameManager.Instance.levelName.Split('.');
        int currentLevel = int.Parse(currentLevelText[1]);

        //If next level can be loaded, load that. If not, load main menu
        if ((currentLevel + 1) <= 20)
        {
            GameManager.Instance.LoadLevel(currentLevelText[0] + '.' + (currentLevel + 1).ToString(), false);
            twoStar.SetActive(true);
            threeStar.SetActive(true);
        }
        else
            GameManager.Instance.LoadMenu();
    }
    private void LoadMenu()
    {
        GameManager.Instance.SmallVibrate();
        GameManager.Instance.LoadMenu();
    }
}
