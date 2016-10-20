using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour {
	public string sceneToLoad;
	public int rotationSpeed;
    GameObject levelPassedScreen;
    LevelStarSystem LevelStarSystem;
    private bool levelPack1 = false, levelPack2 = false, levelPack3 = false;

    void Start()
    {
        levelPassedScreen = GameObject.Find("StarSystem");
        LevelStarSystem = GameObject.Find("StarSystem").GetComponent<LevelStarSystem>();
        //Check what levelpack player is playing
        if (SceneManager.GetActiveScene().buildIndex <= 20)
            levelPack1 = true;
        else if (SceneManager.GetActiveScene().buildIndex > 20)
            levelPack2 = true;
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
            if (GameManager.sharedGM.LevelPack1Stars[SceneManager.GetActiveScene().buildIndex - 1] < (LevelStarSystem.stars))
            {
                GameManager.sharedGM.LevelPack1Stars[SceneManager.GetActiveScene().buildIndex - 1] = (LevelStarSystem.stars);
                GameManager.sharedGM.LevelPack1Stars[SceneManager.GetActiveScene().buildIndex ] = 0;
            }
        if (levelPack2)
            if (GameManager.sharedGM.LevelPack2Stars[SceneManager.GetActiveScene().buildIndex - 21] < (LevelStarSystem.stars))
            {
                GameManager.sharedGM.LevelPack2Stars[SceneManager.GetActiveScene().buildIndex - 21] = (LevelStarSystem.stars);
                GameManager.sharedGM.LevelPack2Stars[SceneManager.GetActiveScene().buildIndex - 20] = 0;

            }
        levelPassedScreen.GetComponent<LevelStarSystem>().showLevelPassedScreen();
        GameManager.sharedGM.Save();
	}
    public void nextLevel()
    {
        //If next level is pressed on the levelpassedpanel, load new scene
        SceneManager.LoadScene (sceneToLoad);
    }
}
