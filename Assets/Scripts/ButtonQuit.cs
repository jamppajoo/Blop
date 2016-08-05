using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonQuit : MonoBehaviour {

    public Button QuitButton;

    void Start()
    {
        QuitButton = GameObject.Find("Quit").GetComponent<Button>();
        QuitButton.onClick.AddListener(() => QuitScene());
    }
    void QuitScene()
    {
        Application.Quit();
    }
}
