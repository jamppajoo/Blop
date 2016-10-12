using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {
    
    public void Start()
    {
    }

	public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
        
    }
}
