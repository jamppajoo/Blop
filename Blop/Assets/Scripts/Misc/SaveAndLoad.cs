using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveAndLoad : MonoBehaviour
{
    #region Singleton

    private static SaveAndLoad _instance;

    public static SaveAndLoad Instance { get { return _instance; } }

    #endregion


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
    }
    //Save and load methods, so that player's progress does not get lost
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData data = new PlayerData();

        for (int i = 0; i < GameManager.Instance.levelsStarAmount.Length; i++)
            data.LevelsStarAmount[i] = GameManager.Instance.levelsStarAmount[i];

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

            for (int i = 0; i < data.LevelsStarAmount.Length; i++)
                GameManager.Instance.levelsStarAmount[i] = data.LevelsStarAmount[i];
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

    //Class which contains player's saved data
    [Serializable]
    class PlayerData
    {
        public int[] LevelsStarAmount = new int[GameManager.Instance.levelsStarAmount.Length];
    }

}
