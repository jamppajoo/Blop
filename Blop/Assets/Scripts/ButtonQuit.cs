using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonQuit : MonoBehaviour {

    private Button QuitButton;
    private GameObject levelPackSelect;

    void Start()
    {
        QuitButton = GameObject.Find("Quit").GetComponent<Button>();
        levelPackSelect = GameObject.Find("LevelPackSelect");
        QuitButton.onClick.AddListener(() => pressedQuit());
       
    }
    void pressedQuit()
    {
        if (levelPackSelect.activeSelf)
        {
            Application.Quit();
        }
    }
    
}
