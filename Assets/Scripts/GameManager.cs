using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour {

    public int[] LevelPack1Stars;
    public int[] LevelPack2Stars;

    public int buttonPressesMax;

    public static int totalButtonPressesLeft = 200;
    private Text buttonPressesLeftText;
    private static Canvas gameManagerCanvas;
    
    public static GameManager sharedGM;
    void Start() {
        gameManagerCanvas = gameObject.transform.GetChild(0).GetComponent<Canvas>() ;
        buttonPressesLeftText = gameManagerCanvas.transform.GetChild(0).GetComponent<Text>();
        
        if(sharedGM == null)
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

    // Update is called once per frame
    void Update () {
        buttonPressesLeftText.text = totalButtonPressesLeft.ToString() + "/" + buttonPressesMax;
	
	}
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData data = new PlayerData();
        for(int i = 0; i < LevelPack1Stars.Length;i++)
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
        if(File.Exists(Application.persistentDataPath + "/playerinfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData) bf.Deserialize(file);
            file.Close();
            for(int i = 0; i < data.LevelPack1Stars.Length;i++)
                LevelPack1Stars[i] = data.LevelPack1Stars[i];
            for (int i = 0; i < data.LevelPack2Stars.Length; i++)
                LevelPack2Stars[i] = data.LevelPack2Stars[i];
            buttonPressesMax = data.buttonPressesMax;
            totalButtonPressesLeft = data.totalButtonPressesLeft;
        }

    }
}

[Serializable]
class PlayerData
{
    public int[] LevelPack1Stars = new int[20];
    public int[] LevelPack2Stars = new int[20];
    public int buttonPressesMax;
    public int totalButtonPressesLeft;

}
