using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {

	void OnTriggerEnter(Collider c)
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}
}
