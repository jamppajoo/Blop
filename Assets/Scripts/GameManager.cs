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

    private int totalStarAmount;

    public int buttonPressesMax;
    public int adRewardAmount;
    public string zoneId;

    public static int totalButtonPressesLeft = 200;
    private Text buttonPressesLeftText, moreJumpsText;
    private float moreJumpsIn;
    public float moreJumpsDelay;
    private static Canvas gameManagerCanvas;
    private Transform adsMenu;

    private Text toastMessage;
    private Image toastMessageBG;

    public static GameManager sharedGM;
    void Awake()
    {
        gameManagerCanvas = gameObject.transform.GetChild(0).GetComponent<Canvas>();

        toastMessageBG = gameManagerCanvas.transform.GetChild(0).GetComponent<Image>();
        toastMessage = gameManagerCanvas.transform.GetChild(1).GetComponent<Text>();

        adsMenu = gameManagerCanvas.transform.GetChild(2);
        adsMenu.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate () { ShowAd(); });
        adsMenu.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => GoToMenu());

        buttonPressesLeftText = gameManagerCanvas.transform.GetChild(3).GetComponent<Text>();
        moreJumpsText = buttonPressesLeftText.transform.GetChild(1).GetComponent<Text>();

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

    }
    
    void Update()
    {
        buttonPressesLeftText.text = totalButtonPressesLeft.ToString() + "/" + buttonPressesMax;
        if (totalButtonPressesLeft <= 0)
            ShowAdMenu();
        if(moreJumpsIn > 0)
        {
            moreJumpsIn -= Time.deltaTime;
        }
        else if (moreJumpsIn <= 0)
        {
            moreJumpsIn = moreJumpsDelay;
            if (totalButtonPressesLeft < buttonPressesMax + 10)
                totalButtonPressesLeft += 20;
        }
        moreJumpsText.text = moreJumpsIn.ToString();
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
        return totalStarAmount;
    }

    //This is called when player runs out of moves
    public void ShowAdMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            if(totalButtonPressesLeft <=0)
            {
                adsMenu.gameObject.SetActive(true);
                MobileControllers.Up.interactable = false;
                MobileControllers.Down.interactable = false;
                MobileControllers.Left.interactable = false;
                MobileControllers.Right.interactable = false;
                MobileControllers.Back.interactable = false;
            }
            else if(totalButtonPressesLeft > 0)
            {
                adsMenu.gameObject.SetActive(false);
                MobileControllers.Up.interactable = true;
                MobileControllers.Down.interactable = true;
                MobileControllers.Left.interactable = true;
                MobileControllers.Right.interactable = true;
                MobileControllers.Back.interactable = true;
            }
            

            if(adsMenu.transform.GetChild(1).GetComponent<Button>())
            {
                if (string.IsNullOrEmpty(zoneId)) zoneId = null;
                adsMenu.transform.GetChild(1).GetComponent<Button>().interactable = Advertisement.IsReady(zoneId);
            }

        }
        else {
            adsMenu.gameObject.SetActive(false);
        }
    }
    public void ShowAd()
    {
        if (string.IsNullOrEmpty(zoneId))
            zoneId = null;
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;
        Advertisement.Show(zoneId, options);
    }
    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                totalButtonPressesLeft += adRewardAmount;
                Save();
                ShowAdMenu();
                StartCoroutine(ShowToastMessage("Added 100 presses"));
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

    public IEnumerator ShowToastMessage(string toastMessageVariable)
    {
        float time = 3;
        float elapsedTime =0;

        print("Showtoast");
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
        data.buttonPressesMax = buttonPressesMax;
        data.totalButtonPressesLeft = totalButtonPressesLeft;

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
            buttonPressesMax = data.buttonPressesMax;
            totalButtonPressesLeft = data.totalButtonPressesLeft;
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
            print("ASD" );
            
        }
    }
}

//Class witch contains player's saved data
[Serializable]
class PlayerData
{
    public int[] LevelPack1Stars = new int[20];
    public int[] LevelPack2Stars = new int[20];
    public int buttonPressesMax;
    public int totalButtonPressesLeft;

}
