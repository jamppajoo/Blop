using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
    [HideInInspector]
    public string myLevelName;
    [HideInInspector]
    public int myLevelNumber;
    [HideInInspector]
    public int myLevelpackNumber;

    [Tooltip("Star images")]
    [SerializeField]
    private Image oneStar, twoStar, threeStar;

    [Tooltip("Star images to place on the positions")]
    [SerializeField]
    private Sprite starSprite, starPlaceSprite;

    [Tooltip("Materials to show player that is button pressable or not")]
    [SerializeField]
    private Material activatedMaterial, deActivatedMaterial;

    private int myStarAmount = 4;

    private Button myButton;
    private Text myText;
    private Renderer myRenderer;

    private LevelSelect levelSelect;

    private void Awake()
    {
        myText = GetComponentInChildren<Text>();

        levelSelect = FindObjectOfType<LevelSelect>();
        myRenderer = GetComponent<Renderer>();

        myLevelName = gameObject.name;
        GetMyLevelNumbers();
        SetMyLevelNumberText();
        SetStarAmount(4);
    }

    private void OnMouseUpAsButton()
    {
        StartLevel();
    }

    private void StartLevel()
    {
        if (myStarAmount != 4)
        {
            levelSelect.StartScene("Level" + myLevelName);
        }
    }

    public void SetStarAmount(int stars)
    {
        myStarAmount = stars;

        oneStar.sprite = starPlaceSprite;
        twoStar.sprite = starPlaceSprite;
        threeStar.sprite = starPlaceSprite;

        oneStar.color = Color.white;
        twoStar.color = Color.white;
        threeStar.color = Color.white;

        ActivateObject();

        switch (stars)
        {
            case 1:
                oneStar.sprite = starSprite;
                break;
            case 2:
                oneStar.sprite = starSprite;
                twoStar.sprite = starSprite;
                break;
            case 3:
                oneStar.sprite = starSprite;
                twoStar.sprite = starSprite;
                threeStar.sprite = starSprite;
                break;
            case 4:
                oneStar.color = Color.clear;
                twoStar.color = Color.clear;
                threeStar.color = Color.clear;
                DeactivateObject();
                break;
        }

    }

    private void DeactivateObject()
    {
        myRenderer.material = deActivatedMaterial;
    }

    private void ActivateObject()
    {
        myRenderer.material = activatedMaterial;
    }
    //Get level numbers by parsing buttons name
    private void GetMyLevelNumbers()
    {
        Int32.TryParse(myLevelName.Split('.')[0], out myLevelpackNumber);
        Int32.TryParse(myLevelName.Split('.')[1], out myLevelNumber);
    }

    private void SetMyLevelNumberText()
    {
        myText.text = (myLevelpackNumber * myLevelNumber).ToString();
    }
}
