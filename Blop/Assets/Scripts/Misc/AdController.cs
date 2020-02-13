using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdController : MonoBehaviour
{
    #region Singleton

    private static AdController _instance;

    public static AdController Instance { get { return _instance; } }

    #endregion

    public bool isShowing = false;

    public Button adsMenuAcceptAdButton, adsMenuDeclineAdButton;
    private string zoneId;

    private void Awake()
    {
        #region Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
        #endregion

        adsMenuAcceptAdButton.onClick.AddListener(ShowAd);
        adsMenuDeclineAdButton.onClick.AddListener(DeclineAd);

    }

    private void Start()
    {
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

    private void DeclineAd()
    {
        DisappearMenu();
    }

    private void ShowAd()
    {
        Debug.Log("Player watched an ad");
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
        GameManager.Instance.hintsLeft++;
        SaveAndLoad.Instance.Save();
        GameManager.Instance.HintUsed();
        EventManager.WatchedAd();
        DisappearMenu();
    }
}
