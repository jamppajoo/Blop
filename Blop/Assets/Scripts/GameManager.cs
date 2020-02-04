using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
    Handles stars and saves them to the file when Save() function is called. When Load() function is called, it loads information from file.
    This Gameobject is singleton, so it's always running on background.
    Everything in GameManager has to be in specific order in Unity becouse of GetChild() methods.
*/

public class GameManager : Singleton<GameManager>
{
    public int[] LevelPack1Stars;
    public int[] LevelPack2Stars;
    public int[] LevelPack3Stars;

    private int totalStarAmount;
    
    public float moreJumpsIn;
    private static Canvas gameManagerCanvas;
    private Transform adsMenu;

    private Text toastMessage;
    private Image toastMessageBG;

    void Awake()
    {
        gameManagerCanvas = gameObject.transform.GetChild(0).GetComponent<Canvas>();

        toastMessageBG = gameManagerCanvas.transform.GetChild(0).GetComponent<Image>();
        toastMessage = gameManagerCanvas.transform.GetChild(1).GetComponent<Text>();

        adsMenu = gameManagerCanvas.transform.GetChild(2);
        adsMenu.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate () { ShowAd(); });
        adsMenu.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => GoToMenu());

        SaveAndLoad.Instance.Load();

    }
    public int ReturnTotalStarAmount()
    {
        totalStarAmount = 0;

        for (int i = 0; i < LevelPack1Stars.Length; i++)
        {
            if (LevelPack1Stars[i] != 4)
                totalStarAmount += LevelPack1Stars[i];
        }
        for (int i = 0; i < LevelPack2Stars.Length; i++)
        {
            if (LevelPack2Stars[i] != 4)
                totalStarAmount += LevelPack2Stars[i];
        }
        for (int i = 0; i < LevelPack3Stars.Length; i++)
        {
            if (LevelPack3Stars[i] != 4)
                totalStarAmount += LevelPack3Stars[i];
        }
        return totalStarAmount;
    }
    
    private void ShowAd()
    {
        AdController.Instance.ShowAd();
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    #region oldToastShit
    //"Toast" to express player that it gained some moves. Also fading in this function
    //public void ShowtoastMessage(string toastMessage)
    //{
    //    StartCoroutine(ShowToastMessage2(toastMessage));
    //}
    //public IEnumerator ShowToastMessage2(string toastMessageVariable)
    //{
    //    float time = 3;
    //    float elapsedTime =0;

    //    toastMessage.text = toastMessageVariable;
    //    while(time > elapsedTime)
    //    {

    //        elapsedTime += Time.fixedDeltaTime;
    //        toastMessage.gameObject.SetActive(true);
    //        toastMessageBG.gameObject.SetActive(true);

    //        if (elapsedTime >1)
    //        {
    //            toastMessageBG.GetComponent<CanvasRenderer>().SetAlpha(toastMessageBG.GetComponent<CanvasRenderer>().GetAlpha() - .01f);
    //            toastMessage.GetComponent<CanvasRenderer>().SetAlpha(toastMessageBG.GetComponent<CanvasRenderer>().GetAlpha() - .01f);
    //        }
    //        yield return null;
    //    }
    //    toastMessage.gameObject.SetActive(false);
    //    toastMessageBG.gameObject.SetActive(false);
    //    elapsedTime = 0;
    //}
    #endregion

}

