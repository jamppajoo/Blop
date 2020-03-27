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

    private RectTransform myRectTransform;
    private Vector3 rectTransformOriginalPosition;

    private void Awake()
    {
        myRectTransform = gameObject.GetComponent<RectTransform>();
        backButton.onClick.AddListener(DisappearMenu);
    }
    private void Start()
    {
        //Disappear menu without activating vibrate
        isShowing = false;
        rectTransformOriginalPosition = myRectTransform.localPosition;
        myRectTransform.gameObject.SetActive(false);
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
        myRectTransform.localPosition = rectTransformOriginalPosition;
        myRectTransform.gameObject.SetActive(false);
    }
    private void ShowMenu()
    {
        if (isShowing)
        {
            DisappearMenu();
            return;
        }

        isShowing = true;
        myRectTransform.localPosition = Vector3.zero;
        myRectTransform.gameObject.SetActive(true);
    }
}
