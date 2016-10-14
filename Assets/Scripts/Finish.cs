using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour {
	public string sceneToLoad;
	public int rotationSpeed;
    GameObject levelPassedScreen;

    void Start()
    {
        levelPassedScreen = GameObject.Find("StarSystem");
    }
	void Update()
	{
		transform.Rotate (Vector3.up * Time.deltaTime*rotationSpeed);
	}
	void OnTriggerEnter(Collider c)
    {
        //Assing star amount to gamemanager
        if (GameObject.Find("GameManager").GetComponent<GameManager>().LevelPack1Stars[SceneManager.GetActiveScene().buildIndex - 1] < (GameObject.Find("StarSystem").GetComponent<LevelStarSystem>().stars))
           GameObject.Find("GameManager").GetComponent<GameManager>().LevelPack1Stars[SceneManager.GetActiveScene().buildIndex-1] = (GameObject.Find("StarSystem").GetComponent<LevelStarSystem>().stars);
        
        levelPassedScreen.GetComponent<LevelStarSystem>().showLevelPassedScreen();
	}
    public void nextLevel()
    {

        //If next level is pressed on the levelpassedpanel, load new scene
        SceneManager.LoadScene (sceneToLoad);
    }
}
