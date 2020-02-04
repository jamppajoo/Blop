using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveAndLoad : Singleton<SaveAndLoad>
{
    //Save and load methods, so that player's progress does not get lost
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData data = new PlayerData();

        for (int i = 0; i < GameManager.Instance.LevelPack1Stars.Length; i++)
            data.LevelPack1Stars[i] = GameManager.Instance.LevelPack1Stars[i];

        for (int i = 0; i < GameManager.Instance.LevelPack2Stars.Length; i++)
            data.LevelPack2Stars[i] = GameManager.Instance.LevelPack2Stars[i];

        for (int i = 0; i < GameManager.Instance.LevelPack3Stars.Length; i++)
            data.LevelPack3Stars[i] = GameManager.Instance.LevelPack3Stars[i];

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
                GameManager.Instance.LevelPack1Stars[i] = data.LevelPack1Stars[i];

            for (int i = 0; i < data.LevelPack2Stars.Length; i++)
                GameManager.Instance.LevelPack2Stars[i] = data.LevelPack2Stars[i];

            for (int i = 0; i < data.LevelPack3Stars.Length; i++)
                GameManager.Instance.LevelPack3Stars[i] = data.LevelPack3Stars[i];
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
        public int[] LevelPack1Stars = new int[GameManager.Instance.LevelPack1Stars.Length];
        public int[] LevelPack2Stars = new int[GameManager.Instance.LevelPack2Stars.Length];
        public int[] LevelPack3Stars = new int[GameManager.Instance.LevelPack3Stars.Length];
    }

}
