using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelPackSelect : MonoBehaviour {

    public Button LevelPackButton;
    private Button temp;
    public GameObject levels, backToLevelSelect;
    public int unlockLevelPackStarAmount ;
    private GameObject levelPackSelect;
    private Text starsText;

    void Start()
    {
        LevelPackButton = GetComponent<Button>();
        LevelPackButton.onClick.AddListener(() => { LevelPackButtonPressed(); });
        temp = backToLevelSelect.GetComponent<Button>();
        levelPackSelect = GameObject.Find("LevelPackSelect");

        starsText = gameObject.transform.GetChild(1).GetComponent<Text>();
        starsText.text = GameManager.Instance.ReturnTotalStarAmount().ToString() + "/" + unlockLevelPackStarAmount.ToString();

        if (unlockLevelPackStarAmount > GameManager.Instance.ReturnTotalStarAmount())
            gameObject.GetComponent<Button>().interactable = false;

        else
        {
            gameObject.GetComponent<Button>().interactable = true;
            starsText.gameObject.SetActive(false);
        }
        temp.onClick.AddListener(() => { BackButtonPressed(); });
        }
    public void LevelPackButtonPressed()
    {
        levels.SetActive(true);
        levelPackSelect.SetActive(false);
        backToLevelSelect.SetActive(true);
    }
    public void BackButtonPressed()
    {
        levels.SetActive(false);
        levelPackSelect.SetActive(true);
        backToLevelSelect.SetActive(false);
    }
}
