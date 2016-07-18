using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {
    
    public void Start()
    {
        LoadScene("null");
    }

	public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
