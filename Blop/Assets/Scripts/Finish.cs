using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour {
	public string sceneToLoad;
	public int rotationSpeed;
    GameObject levelPassedScreen;
    LevelStarSystem LevelStarSystem;
    private bool levelPack1 = false, levelPack2 = false, levelPack3 = false;
    private int ActiveSceneBuildIndex;


    void Start()
    {
        levelPassedScreen = GameObject.Find("StarSystem");
        LevelStarSystem = GameObject.Find("StarSystem").GetComponent<LevelStarSystem>();
        //Check what levelpack player is playing
        if (SceneManager.GetActiveScene().buildIndex <= 20)
            levelPack1 = true;
        else if (SceneManager.GetActiveScene().buildIndex >= 21 && SceneManager.GetActiveScene().buildIndex <= 23)
            levelPack2 = true;
        else if (SceneManager.GetActiveScene().buildIndex > 23)
            levelPack3 = true;
        ActiveSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
    }
    void Update()
	{
        //Rotate finish block
		transform.Rotate (Vector3.up * Time.deltaTime*rotationSpeed);
	}
	void OnTriggerEnter(Collider c)
    {
        //Assing star amount to GameManager if star amount is bigger than in there.
        if (levelPack1)
            if (GameManager.Instance.LevelPack1Stars[ActiveSceneBuildIndex - 1] < (LevelStarSystem.stars))
            {
                if (LevelStarSystem.stars == 3)
                {
                    //Add new system to reward player
                }

                GameManager.Instance.LevelPack1Stars[ActiveSceneBuildIndex - 1] = (LevelStarSystem.stars);
                //if next levels star amount is over 3, make it zero
                if(ActiveSceneBuildIndex != GameManager.Instance.LevelPack1Stars.Length)
                    if(GameManager.Instance.LevelPack1Stars[ActiveSceneBuildIndex] > 3)
                        GameManager.Instance.LevelPack1Stars[ActiveSceneBuildIndex] = 0;
            }
        if (levelPack2)
            if (GameManager.Instance.LevelPack2Stars[ActiveSceneBuildIndex - 21] < (LevelStarSystem.stars))
            {
                if (LevelStarSystem.stars == 3)
                {
                    //Add new system to reward player
                }
                GameManager.Instance.LevelPack2Stars[ActiveSceneBuildIndex - 21] = (LevelStarSystem.stars);
                //if next levels star amount is over 3, make it zero
                if (ActiveSceneBuildIndex - 20 != GameManager.Instance.LevelPack2Stars.Length)
                    if (GameManager.Instance.LevelPack2Stars[ActiveSceneBuildIndex - 20] > 3)
                    GameManager.Instance.LevelPack2Stars[ActiveSceneBuildIndex - 20] = 0;

            }
        if (levelPack3)
            if (GameManager.Instance.LevelPack3Stars[ActiveSceneBuildIndex - 24] < (LevelStarSystem.stars))
            {
                if (LevelStarSystem.stars == 3)
                {
                    //Add new system to reward player
                }
                GameManager.Instance.LevelPack3Stars[ActiveSceneBuildIndex - 24] = (LevelStarSystem.stars);
                //if next levels star amount is over 3, make it zero
                if (ActiveSceneBuildIndex - 23 != GameManager.Instance.LevelPack3Stars.Length)
                    if (GameManager.Instance.LevelPack3Stars[ActiveSceneBuildIndex - 23] > 3)
                    GameManager.Instance.LevelPack3Stars[ActiveSceneBuildIndex - 23] = 0;
            }

        levelPassedScreen.GetComponent<LevelStarSystem>().showLevelPassedScreen();
        SaveAndLoad.Instance.Save();
	}
    public void nextLevel()
    {
        //If next level is pressed on the levelpassedpanel, load new scene
        SceneManager.LoadScene (sceneToLoad);
    }
}
