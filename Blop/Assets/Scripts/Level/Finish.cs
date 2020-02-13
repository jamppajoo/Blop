using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class Finish : MonoBehaviour
{
    //public string sceneToLoad;
    public int rotationSpeed;
    private LevelStarSystem levelStarSystem;
    private bool levelPack1 = false, levelPack2 = false, levelPack3 = false;
    private int activeSceneBuildIndex;


    void Start()
    {
        levelStarSystem = FindObjectOfType<LevelStarSystem>();

        activeSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        //Check what levelpack player is playing
        if (activeSceneBuildIndex <= 20)
            levelPack1 = true;
        else if (activeSceneBuildIndex >= 21 && SceneManager.GetActiveScene().buildIndex <= 23)
            levelPack2 = true;
        else if (activeSceneBuildIndex > 23)
            levelPack3 = true;
    }
    void Update()
    {
        //Rotate finish block
        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
    }
    void OnTriggerEnter(Collider c)
    {
        //Assing star amount to GameManager if star amount is bigger than in there.
        if (levelPack1)
            if (GameManager.Instance.levelPack1Stars[activeSceneBuildIndex - 1] < (levelStarSystem.stars))
            {
                if (levelStarSystem.stars == 3)
                {
                    //Add new system to reward player
                }

                GameManager.Instance.levelPack1Stars[activeSceneBuildIndex - 1] = (levelStarSystem.stars);
                //if next levels star amount is over 3, make it zero
                if (activeSceneBuildIndex != GameManager.Instance.levelPack1Stars.Length)
                    if (GameManager.Instance.levelPack1Stars[activeSceneBuildIndex] > 3)
                        GameManager.Instance.levelPack1Stars[activeSceneBuildIndex] = 0;
            }
        if (levelPack2)
            if (GameManager.Instance.levelPack2Stars[activeSceneBuildIndex - 21] < (levelStarSystem.stars))
            {
                if (levelStarSystem.stars == 3)
                {
                    //Add new system to reward player
                }
                GameManager.Instance.levelPack2Stars[activeSceneBuildIndex - 21] = (levelStarSystem.stars);
                //if next levels star amount is over 3, make it zero
                if (activeSceneBuildIndex - 20 != GameManager.Instance.levelPack2Stars.Length)
                    if (GameManager.Instance.levelPack2Stars[activeSceneBuildIndex - 20] > 3)
                        GameManager.Instance.levelPack2Stars[activeSceneBuildIndex - 20] = 0;

            }
        if (levelPack3)
            if (GameManager.Instance.levelPack3Stars[activeSceneBuildIndex - 24] < (levelStarSystem.stars))
            {
                if (levelStarSystem.stars == 3)
                {
                    //Add new system to reward player
                }
                GameManager.Instance.levelPack3Stars[activeSceneBuildIndex - 24] = (levelStarSystem.stars);
                //if next levels star amount is over 3, make it zero
                if (activeSceneBuildIndex - 23 != GameManager.Instance.levelPack3Stars.Length)
                    if (GameManager.Instance.levelPack3Stars[activeSceneBuildIndex - 23] > 3)
                        GameManager.Instance.levelPack3Stars[activeSceneBuildIndex - 23] = 0;
            }

        levelStarSystem.ShowLevelPassedScreen();
        SaveAndLoad.Instance.Save();
    }
    public void NextLevel()
    {
        //If next level is pressed on the levelpassedpanel, load new scene
        string[] currentLevelText = SceneManager.GetActiveScene().name.Split('.');
        int currentLevel = int.Parse(currentLevelText[1]);
        currentLevel++;

        if (Application.CanStreamedLevelBeLoaded(currentLevelText[0] + '.' + currentLevel.ToString()))
        {
            SceneManager.LoadScene(currentLevelText[0] + '.' + currentLevel.ToString());
        }
        else
            GameManager.Instance.LoadMenu();
        
    }
}
