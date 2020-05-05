using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handle showing stars at the end of the level, and keeping track on how many stars currently player is getting
/// </summary>

public class LevelStarSystem : MonoBehaviour
{
    [HideInInspector]
    public int stars;

    [SerializeField]
    private Button nextLevel, restartLevel, menuButton;
    [SerializeField]
    private GameObject oneStar, twoStar, threeStar, levelPassedPanel;

    private int threeStarMovementAmount, twoStarMovementAmount;
    private int buttonPressesAmount = 0;

    private MobileControllers mobileControllers;
    private Finish finish;
    private LevelStats levelStats;
    private RectTransform myRectTransform;

    private Vector3 rectTransformOriginalPosition;


    private void Awake()
    {
        myRectTransform = gameObject.GetComponent<RectTransform>();
        finish = FindObjectOfType<Finish>();
        stars = 3;
        mobileControllers = FindObjectOfType<MobileControllers>();
        nextLevel.onClick.AddListener(NextLevel);
        restartLevel.onClick.AddListener(Restart);
        menuButton.onClick.AddListener(LoadMenu);
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
        rectTransformOriginalPosition = myRectTransform.localPosition;
        myRectTransform.gameObject.SetActive(false);
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

        //Player always gets at least one star
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
                threeStar.SetActive(false);
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
        myRectTransform.localPosition = Vector3.zero;
        myRectTransform.gameObject.SetActive(true);
        mobileControllers.gameObject.SetActive(false);
    }

    private void Restart()
    {
        GameManager.Instance.SmallVibrate();
        mobileControllers.gameObject.SetActive(true);
        myRectTransform.localPosition = rectTransformOriginalPosition;
        myRectTransform.gameObject.SetActive(false);

        twoStar.SetActive(true);
        threeStar.SetActive(true);
        GameManager.Instance.LoadLevel(GameManager.Instance.levelName, false);
    }

    private void NextLevel()
    {
        GameManager.Instance.SmallVibrate();
        mobileControllers.gameObject.SetActive(true);
        myRectTransform.localPosition = rectTransformOriginalPosition;
        myRectTransform.gameObject.SetActive(false);
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
