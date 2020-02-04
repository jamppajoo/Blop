using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdController : Singleton<AdController>
{

    private string zoneId;

    public void ShowAd()
    {
        if (string.IsNullOrEmpty(zoneId))
            zoneId = null;
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;
        Advertisement.Show(zoneId, options);
    }

    //TODO Add new ad scripts
    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                SaveAndLoad.Instance.Save();
                //StartCoroutine(ShowToastMessage("Added 100 presses"));
                break;
            case ShowResult.Skipped:
                Debug.LogWarning("Video was skipped.");
                break;
            case ShowResult.Failed:
                Debug.LogError("Video failed to show.");
                break;
        }
    }
}
