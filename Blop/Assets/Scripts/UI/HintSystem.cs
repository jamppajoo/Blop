using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handle the hint system how to use menu
/// </summary>
public class HintSystem : MonoBehaviour
{
    public bool isShowing = false;

    [SerializeField]
    private Button backButton;

    private void Awake()
    {
        backButton.onClick.AddListener(DisappearMenu);
    }
    private void Start()
    {
        //Disappear menu without activating vibrate
        isShowing = false;
        gameObject.SetActive(false);

    }
    public void ShowMenu(bool show)
    {
        if (!show)
            DisappearMenu();
        else
            ShowMenu();

    }
    private void DisappearMenu()
    {
        GameManager.Instance.SmallVibrate();
        isShowing = false;
        gameObject.SetActive(false);
    }
    private void ShowMenu()
    {
        if (isShowing)
        {
            DisappearMenu();
            return;
        }

        isShowing = true;
        gameObject.SetActive(true);
    }
}
