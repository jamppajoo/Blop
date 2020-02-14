using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public int rotationSpeed;
    private LevelStarSystem levelStarSystem;
    private int activeSceneBuildIndex;

    void Start()
    {
        levelStarSystem = FindObjectOfType<LevelStarSystem>();

        activeSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
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
        if (GameManager.Instance.levelsStarAmount[activeSceneBuildIndex - 1] < (levelStarSystem.stars))
        {
            if (levelStarSystem.stars == 3)
            {
                GameManager.Instance.AddHint();
            }

            GameManager.Instance.levelsStarAmount[activeSceneBuildIndex - 1] = (levelStarSystem.stars);
            //if next levels star amount is over 3, make it zero to indicate that level is unlocked
            if (activeSceneBuildIndex != GameManager.Instance.levelsStarAmount.Length)
                if (GameManager.Instance.levelsStarAmount[activeSceneBuildIndex] > 3)
                    GameManager.Instance.levelsStarAmount[activeSceneBuildIndex] = 0;
        }

        levelStarSystem.ShowLevelPassedScreen();
        GameManager.Instance.hintActive = false;
        SaveAndLoad.Instance.Save();
    }
    public void NextLevel()
    {
        //If next level is pressed on the levelpassedpanel, load new scene
        string[] currentLevelText = SceneManager.GetActiveScene().name.Split('.');
        int currentLevel = int.Parse(currentLevelText[1]);

        //If next level can be loaded, load that. If not, load main menu
        if (Application.CanStreamedLevelBeLoaded(currentLevelText[0] + '.' + (currentLevel + 1).ToString()))
        {
            SceneManager.LoadScene(currentLevelText[0] + '.' + (currentLevel + 1).ToString());
        }
        else
            GameManager.Instance.LoadMenu();

    }
}
