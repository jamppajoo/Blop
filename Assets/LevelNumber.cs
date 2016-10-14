using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelNumber : MonoBehaviour {

	void Start () {
        gameObject.GetComponent<Text>().text = SceneManager.GetActiveScene().name;
	}

}
