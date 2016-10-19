using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int[] LevelPack1Stars;
    public int[] LevelPack2Stars;

    public int buttonPressesMax;

    public static int totalButtonPressesLeft = 200;
    private Text buttonPressesLeftText;
    private static Canvas gameManagerCanvas;
    
    private static GameManager sharedGM;
    // Use this for initialization
    void Start() {
        
        buttonPressesLeftText = GameObject.Find("ButtonPressesLeftText").GetComponent<Text>();
        gameManagerCanvas = GameObject.Find("GMCanvas").GetComponent<Canvas>();
        if(sharedGM == null)
        {
            sharedGM = this;
            DontDestroyOnLoad(sharedGM);
        }
        else
        {
            Destroy(this.gameObject);
            
        }
    }

    // Update is called once per frame
    void Update () {
        buttonPressesLeftText.text = totalButtonPressesLeft.ToString() + "/" + buttonPressesMax;
	
	}
}
