using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour {
	public string sceneToLoad;
	public int rotationSpeed;
    GameObject levelPassedScreen;
    GameManager GameManager;
    LevelStarSystem LevelStarSystem;
    private bool levelPack1 = false, levelPack2 = false, levelPack3 = false;

    void Start()
    {
        levelPassedScreen = GameObject.Find("StarSystem");

        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        LevelStarSystem = GameObject.Find("StarSystem").GetComponent<LevelStarSystem>();


        if (SceneManager.GetActiveScene().buildIndex <= 20)
            levelPack1 = true;
        else if (SceneManager.GetActiveScene().buildIndex > 20)
            levelPack2 = true;
    }
    void Update()
	{
		transform.Rotate (Vector3.up * Time.deltaTime*rotationSpeed);
	}
	void OnTriggerEnter(Collider c)
    {
        //Assing star amount to gamemanager

        if (levelPack1)
            if (GameManager.LevelPack1Stars[SceneManager.GetActiveScene().buildIndex - 1] < (LevelStarSystem.stars))
            {
                GameManager.LevelPack1Stars[SceneManager.GetActiveScene().buildIndex - 1] = (LevelStarSystem.stars);
                GameManager.LevelPack1Stars[SceneManager.GetActiveScene().buildIndex ] = 0;
            }
        if (levelPack2)
            if (GameManager.LevelPack2Stars[SceneManager.GetActiveScene().buildIndex - 21] < (LevelStarSystem.stars))
            {
                GameManager.LevelPack2Stars[SceneManager.GetActiveScene().buildIndex - 21] = (LevelStarSystem.stars);
                GameManager.LevelPack2Stars[SceneManager.GetActiveScene().buildIndex - 20] = 0;

            }
        levelPassedScreen.GetComponent<LevelStarSystem>().showLevelPassedScreen();
	}
    public void nextLevel()
    {

        //If next level is pressed on the levelpassedpanel, load new scene
        SceneManager.LoadScene (sceneToLoad);
    }
}
