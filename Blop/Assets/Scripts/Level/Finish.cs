using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public int rotationSpeed;
    private LevelStarSystem levelStarSystem;
    private int activeLevel;

    void Start()
    {
        levelStarSystem = FindObjectOfType<LevelStarSystem>();

    }
    void Update()
    {
        //Rotate finish block
        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
    }
    void OnTriggerEnter(Collider collider)
    {
        //If not colliding with player, dont run anything
        if (collider.tag != "Player")
            return;
        activeLevel = GameManager.Instance.levelNumber;

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
        AnalyticsManager.Instance.SetStarAmount(levelStarSystem.stars);
        AnalyticsManager.Instance.SendLevelPassingAnalytics();
    }
}
