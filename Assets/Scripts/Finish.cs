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

        levelPassedScreen.GetComponent<LevelStarSystem>().showLevelPassedScreen();
	}
    public void nextLevel()
    {
        //If next level is pressed on the levelpassedpanel, load new scene
        SceneManager.LoadScene (sceneToLoad);
    }
}
