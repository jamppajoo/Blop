using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonQuit : MonoBehaviour
{
    private Button QuitButton;

    void Start()
    {
        QuitButton = gameObject.GetComponent<Button>();
        QuitButton.onClick.AddListener(() => QuitGame());

    }
    void QuitGame()
    {
        GameManager.Instance.SmallVibrate();
        Application.Quit();

    }

}
