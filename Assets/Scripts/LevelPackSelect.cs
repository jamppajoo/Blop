using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelPackSelect : MonoBehaviour {

    public Button LevelPackButton;
    private Button temp;
    public GameObject levels, backToLevelSelect;
    private GameObject levelPackSelect;

    void Start()
    {
        LevelPackButton = GetComponent<Button>();
        LevelPackButton.onClick.AddListener(() => { LevelPackButtonPressed(); });
        temp = backToLevelSelect.GetComponent<Button>();
        levelPackSelect = GameObject.Find("LevelPackSelect");
        temp.onClick.AddListener(() => { BackButtonPressed(); });

    }
    public void LevelPackButtonPressed()
    {
        levels.SetActive(true);
        levelPackSelect.SetActive(false);
        backToLevelSelect.SetActive(true);
        //temp.onClick.AddListener(() => { BackButtonPressed(); });


    }
    public void BackButtonPressed()
    {
        levels.SetActive(false);
        levelPackSelect.SetActive(true);
        backToLevelSelect.SetActive(false);
    }
}
