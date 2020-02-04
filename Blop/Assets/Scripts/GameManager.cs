using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

/*
    Handles stars, button presses left, button presses max amount and saves them to the file
    when Save() function is called. When Load() function is called, it loads information from 
    file.
    Has variable sharedGM which can be called to directly call variables. (GameManager.sharedGM.buttonPressesMax = 100)
    This Gameobject is't deleted in any point of the game. Allways on the back running.
    Everything in GameManager has to be in specific order in Unity becouse of getchild() methods.
*/

public class GameManager : MonoBehaviour
{

    public int[] LevelPack1Stars;
    public int[] LevelPack2Stars;
    public int[] LevelPack3Stars;

    private int totalStarAmount;
    
    public string zoneId;
    
    public float moreJumpsIn;
    private static Canvas gameManagerCanvas;
    private Transform adsMenu;

    private Text toastMessage;
    private Image toastMessageBG;

    int curTime, savedTime, difTime;

    public GameObject levelPack1;


    public static GameManager sharedGM;


    //public void setVisible()
    //{

    //    foreach (Transform child in levelPack1.transform)
    //    {
    //        child.gameObject.SetActive(true);
    //        print("Näkkyyy");
    //    }

    //}
    //public void setInVisible()
    //{
    //    foreach (Transform child in levelPack1.transform)
    //    {
    //        child.gameObject.SetActive(false);
    //        print("Ei näyyy");
    //    }
    //}



    void Awake()
    {
        gameManagerCanvas = gameObject.transform.GetChild(0).GetComponent<Canvas>();

        toastMessageBG = gameManagerCanvas.transform.GetChild(0).GetComponent<Image>();
        toastMessage = gameManagerCanvas.transform.GetChild(1).GetComponent<Text>();

        adsMenu = gameManagerCanvas.transform.GetChild(2);
        adsMenu.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate () { ShowAd(); });
        adsMenu.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => GoToMenu());

        if (sharedGM == null)
        {
            sharedGM = this;
            DontDestroyOnLoad(sharedGM);
        }
        else
        {
            Destroy(this.gameObject);

        }
        Load();
        //setInVisible();

    }
    
    void Update()
    {
        System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        curTime = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
        
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
    
    public void ShowAd()
    {
        if (string.IsNullOrEmpty(zoneId))
            zoneId = null;
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;
        Advertisement.Show(zoneId, options);
    }
    //Add new ad scripts
    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Save();
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
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    //"Toast" to express player that it gained some moves. Also fading in this function
    public void ShowtoastMessage(string toastMessage)
    {
        StartCoroutine(ShowToastMessage2(toastMessage));
    }
    public IEnumerator ShowToastMessage2(string toastMessageVariable)
    {
        float time = 3;
        float elapsedTime =0;
        
        toastMessage.text = toastMessageVariable;
        while(time > elapsedTime)
        {

            elapsedTime += Time.fixedDeltaTime;
            toastMessage.gameObject.SetActive(true);
            toastMessageBG.gameObject.SetActive(true);

            if (elapsedTime >1)
            {
                toastMessageBG.GetComponent<CanvasRenderer>().SetAlpha(toastMessageBG.GetComponent<CanvasRenderer>().GetAlpha() - .01f);
                toastMessage.GetComponent<CanvasRenderer>().SetAlpha(toastMessageBG.GetComponent<CanvasRenderer>().GetAlpha() - .01f);
            }
            yield return null;
        }
        toastMessage.gameObject.SetActive(false);
        toastMessageBG.gameObject.SetActive(false);
        elapsedTime = 0;
    }

    //Save and load methods, so that player's progress does not get lost
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData data = new PlayerData();
        for (int i = 0; i < LevelPack1Stars.Length; i++)
            data.LevelPack1Stars[i] = LevelPack1Stars[i];
        for (int i = 0; i < LevelPack2Stars.Length; i++)
            data.LevelPack2Stars[i] = LevelPack2Stars[i];
        for (int i = 0; i < LevelPack3Stars.Length; i++)
            data.LevelPack3Stars[i] = LevelPack3Stars[i];
        
        data.savedTime = savedTime;

        bf.Serialize(file, data);

        file.Close();
    }
    
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerinfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            for (int i = 0; i < data.LevelPack1Stars.Length; i++)
                LevelPack1Stars[i] = data.LevelPack1Stars[i];
            for (int i = 0; i < data.LevelPack2Stars.Length; i++)
                LevelPack2Stars[i] = data.LevelPack2Stars[i];
            for (int i = 0; i < data.LevelPack3Stars.Length; i++)
                LevelPack3Stars[i] = data.LevelPack3Stars[i];
            savedTime = data.savedTime;
        }
    }

    //Dev button witch deletes personal data folder, for testing purposes
    public void DeletePersonalData()
    {
        if (File.Exists(Application.persistentDataPath + "/playerinfo.dat"))
        {
            DirectoryInfo dataDir = new DirectoryInfo(Application.persistentDataPath);
            dataDir.Delete(true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        savedTime = curTime;
    }
}

//Class witch contains player's saved data
[Serializable]
class PlayerData
{
    public int[] LevelPack1Stars = new int[GameManager.sharedGM.LevelPack1Stars.Length];
    public int[] LevelPack2Stars = new int[GameManager.sharedGM.LevelPack2Stars.Length];
    public int[] LevelPack3Stars = new int[GameManager.sharedGM.LevelPack3Stars.Length];
    public int savedTime;
}

