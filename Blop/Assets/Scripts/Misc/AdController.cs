using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdController : MonoBehaviour
{

    public bool isShowing = false;

    public Button adsMenuAcceptAdButton, adsMenuDeclineAdButton;
    private string zoneId;

    private RectTransform myRectTransform;
    private Vector3 rectTransformOriginalPosition;

    private void Awake()
    {
        //#region Singleton
        //if (_instance != null && _instance != this)
        //{
        //    Destroy(this.gameObject);
        //    return;
        //}

        //_instance = this;
        //DontDestroyOnLoad(this.gameObject);
        //#endregion

        myRectTransform = gameObject.GetComponent<RectTransform>();
        adsMenuAcceptAdButton.onClick.AddListener(ShowAd);
        adsMenuDeclineAdButton.onClick.AddListener(DeclineAd);

    }

    private void Start()
    {
        rectTransformOriginalPosition = myRectTransform.localPosition;
        DisappearMenu();

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
        isShowing = false;
        myRectTransform.localPosition = rectTransformOriginalPosition;
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
    }

    private void DeclineAd()
    {
        GameManager.Instance.SmallVibrate();
        DisappearMenu();
    }

    private void ShowAd()
    {
        GameManager.Instance.SmallVibrate();
        AddHint();
        //if (string.IsNullOrEmpty(zoneId))
        //    zoneId = null;
        //ShowOptions options = new ShowOptions();
        //options.resultCallback = HandleShowResult;
        //Advertisement.Show(zoneId, options);
    }
    
    private void HandleShowResult(ShowResult result)
    {
        //switch (result)
        //{
        //    case ShowResult.Finished:
        //        AddHint();
        //        break;
        //    case ShowResult.Skipped:
        //        DisappearMenu();
        //        break;
        //    case ShowResult.Failed:
        //        AddHint();
        //        break;
        //}
    }

    private void AddHint()
    {
        SaveAndLoad.Instance.Save();
        GameManager.Instance.HintUsed();
        EventManager.WatchedAd();
        AnalyticsManager.Instance.HintUsed();
        DisappearMenu();
    }
}
