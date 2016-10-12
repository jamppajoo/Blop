using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    
    public int[] LevelPack1Stars = new int[20];
    

    GameManager GM;
    // Use this for initialization
    void Start() {
        DontDestroyOnLoad(this);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
