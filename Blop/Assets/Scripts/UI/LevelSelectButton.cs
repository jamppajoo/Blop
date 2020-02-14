using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
    [HideInInspector]
    public string myLevelName;

    public int myLevelNumber;
    public int myLevelpackNumber;

    [SerializeField]
    private Image oneStar, twoStar, threeStar;

    [SerializeField]
    private Sprite starSprite, starPlaceSprite;

    [SerializeField]
    private Material activatedMaterial, deActivatedMaterial;

    private Button myButton;
    private Text myText;
    private LevelSelect levelSelect;

    private int myStarAmount = 4;

    private Renderer myRenderer;

    private void Awake()
    {
        myButton = GetComponentInChildren<Button>();
        myText = GetComponentInChildren<Text>();

        levelSelect = FindObjectOfType<LevelSelect>();
        myRenderer = GetComponent<Renderer>();

        myLevelName = gameObject.name;
        GetMyLevelNumbers();
        SetMyLevelNumberText();
        SetStarAmount(4);

        myButton.onClick.AddListener(StartLevel);
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
        myButton.interactable = false;
        myRenderer.material = deActivatedMaterial;
    }
    private void ActivateObject()
    {
        myButton.interactable = true;
        myRenderer.material = activatedMaterial;    
    }

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
