using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelNumber : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Text>().text = SceneManager.GetActiveScene().name;
        

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
