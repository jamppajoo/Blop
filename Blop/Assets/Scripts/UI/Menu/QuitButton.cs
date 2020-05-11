using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace  Menu
{ 
public class QuitButton : MonoBehaviour
{
    private Button quitButton;

    void Start()
    {
        quitButton = gameObject.GetComponent<Button>();
        quitButton.onClick.AddListener(() => QuitGame());

    }
    void QuitGame()
    {
        GameManager.Instance.SmallVibrate();
        Application.Quit();

    }

}
}
