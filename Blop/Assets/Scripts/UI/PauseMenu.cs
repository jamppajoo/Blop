using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button restartButton, menuButton, backButton;

    private RectTransform myRectTransform;
    private Vector3 rectTransformOriginalPosition;
    private bool showing = false;

    private void Awake()
    {
        myRectTransform = gameObject.GetComponent<RectTransform>();

        restartButton.onClick.AddListener(Restart);
        menuButton.onClick.AddListener(LoadMenu);
        backButton.onClick.AddListener(BackToGame);
    }

    private void Start()
    {
        rectTransformOriginalPosition = myRectTransform.localPosition;
        myRectTransform.gameObject.SetActive(false);
    }

    public void ShowPauseMenu()
    {
        if (showing)
        {
            DisappearPauseMenu();
            return;
        }
        myRectTransform.localPosition = Vector3.zero;
        myRectTransform.gameObject.SetActive(true);
        showing = true;
    }
    public void DisappearPauseMenu()
    {
        myRectTransform.localPosition = rectTransformOriginalPosition;
        myRectTransform.gameObject.SetActive(false);
        showing = false;

    }
    private void Restart()
    {
        GameManager.Instance.SmallVibrate();
        //mobileControllers.gameObject.SetActive(true);
        myRectTransform.localPosition = rectTransformOriginalPosition;
        myRectTransform.gameObject.SetActive(false);

        //twoStar.SetActive(true);
        //threeStar.SetActive(true);
        GameManager.Instance.LoadLevel(GameManager.Instance.levelName, false);
    }
    private void LoadMenu()
    {
        AnalyticsManager.Instance.SendRageQuitAnalytics();
        GameManager.Instance.SmallVibrate();
        GameManager.Instance.LoadMenu();
    }
    private void BackToGame()
    {
        GameManager.Instance.SmallVibrate();
        DisappearPauseMenu();
    }
}
