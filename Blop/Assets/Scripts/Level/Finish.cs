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

        activeLevel = GameManager.Instance.levelNumber;
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

        //Assing star amount to GameManager if star amount is bigger than in there.
        if (GameManager.Instance.levelsStarAmount[activeLevel] < (levelStarSystem.stars))
        {
            if (levelStarSystem.stars == 3)
            {
                GameManager.Instance.AddHint();
            }

            GameManager.Instance.levelsStarAmount[activeLevel] = (levelStarSystem.stars);
            //if next levels star amount is over 3, make it zero to indicate that level is unlocked
            if (activeLevel + 1 != GameManager.Instance.levelsStarAmount.Length)
                if (GameManager.Instance.levelsStarAmount[activeLevel + 1] > 3)
                    GameManager.Instance.levelsStarAmount[activeLevel + 1] = 0;
        }

        levelStarSystem.ShowLevelPassedScreen();
        GameManager.Instance.hintActive = false;
        SaveAndLoad.Instance.Save();
    }
}
