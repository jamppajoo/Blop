using UnityEngine;

/// <summary>
/// Handle level finish stuff, adding stars, trigger showing menus, trigger sending analytics etc.
/// </summary>
public class Finish : MonoBehaviour
{
    [Tooltip("How fast should the finish block rotate (angles in frames)")]
    [SerializeField]
    private int rotationSpeed;
    private int activeLevel;

    private LevelStarSystem levelStarSystem;
    private void Awake()
    {
        levelStarSystem = FindObjectOfType<LevelStarSystem>();
    }
    private void Update()
    {
        //Rotate finish block
        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
    }

    private void OnTriggerEnter(Collider collider)
    {
        //If not colliding with player, dont run anything
        if (collider.tag != "Player")
            return;
        activeLevel = GameManager.Instance.levelNumber * GameManager.Instance.levelPackNumber;
        GameManager.Instance.levelPlayedAmount[activeLevel - 1]++;
        //Assing star amount to GameManager if star amount is bigger than in there.
        if (GameManager.Instance.levelsStarAmount[activeLevel-1] < (levelStarSystem.stars))
        {
            GameManager.Instance.levelsStarAmount[activeLevel-1] = (levelStarSystem.stars);
            //if next levels star amount is over 3, make it zero to indicate that level is unlocked
            if (activeLevel != GameManager.Instance.levelsStarAmount.Length)
                if (GameManager.Instance.levelsStarAmount[activeLevel] > 3)
                    GameManager.Instance.levelsStarAmount[activeLevel] = 0;
        }

        levelStarSystem.ShowLevelPassedScreen();
        GameManager.Instance.hintActive = false;
        SaveAndLoad.Instance.Save();
        //Set analytics and send them
        AnalyticsManager.Instance.SetPlayedAmount(GameManager.Instance.levelPlayedAmount[activeLevel - 1]);
        AnalyticsManager.Instance.SetStarAmount(levelStarSystem.stars);
        AnalyticsManager.Instance.SendLevelPassingAnalytics();
    }
}
