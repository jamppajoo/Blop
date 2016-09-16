using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour {
	public string sceneToLoad;
	public int rotationSpeed;
    LevelStarSystem starSystem = new LevelStarSystem();

	void Update()
	{
		transform.Rotate (Vector3.up * Time.deltaTime*rotationSpeed);
	}
	void OnTriggerEnter(Collider c)
    {
        //tell star system that level has passed and load new scene
        starSystem.levelPassed();
        SceneManager.LoadScene (sceneToLoad);
        
        

	}
}
