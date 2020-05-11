using System;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class LevelSelectButton : MonoBehaviour
    {
        //[HideInInspector]
        public string myLevelName;
        //[HideInInspector]
        public int myLevelNumber;
        //[HideInInspector]
        public int myLevelPackNumber;

        [Tooltip("Star images")]
        [SerializeField]
        private Image oneStar, twoStar, threeStar;

        //[Tooltip("Star images to place on the positions")]
        //[SerializeField]
        //private Sprite starSprite, starPlaceSprite;

        [Tooltip("Materials to show player that is button pressable or not")]
        [SerializeField]
        private Material activatedMaterial, deActivatedMaterial;

        private int myStarAmount = 4;

        private Button myButton;
        private Text myText;
        //private Renderer myRenderer;

        private LevelSelect levelSelect;

        private void Awake()
        {
            myText = GetComponentInChildren<Text>();
            myButton = GetComponent<Button>();
            myButton.onClick.AddListener(StartLevel);
            levelSelect = FindObjectOfType<LevelSelect>();
            //myRenderer = GetComponent<Renderer>();

            myLevelName = gameObject.name;
            GetMyLevelNumbers();
            SetMyLevelNumberText();
            SetStarAmount(4);
        }

        //private void OnMouseUpAsButton()
        //{
        //    StartLevel();
        //}

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

            oneStar.color = Color.clear;
            twoStar.color = Color.clear;
            threeStar.color = Color.clear;

            ActivateObject();

            switch (stars)
            {
                case 1:
                    oneStar.color = Color.white;
                    break;
                case 2:
                    oneStar.color = Color.white;
                    twoStar.color = Color.white;
                    break;
                case 3:
                    oneStar.color = Color.white;
                    twoStar.color = Color.white;
                    threeStar.color = Color.white;
                    break;
                case 4:
                    oneStar.color = Color.clear;
                    twoStar.color = Color.clear;
                    threeStar.color = Color.clear;
                    DeactivateObject();
                    break;
            }

        }

        public void SetLevelPackNumber(int levelPackNumber)
        {
            myLevelPackNumber = levelPackNumber;
            myLevelName = "" + myLevelPackNumber + '.' + myLevelNumber;
        }
        private void DeactivateObject()
        {
            //myRenderer.material = deActivatedMaterial;
            myButton.interactable = false;
        }

        private void ActivateObject()
        {
            myButton.interactable = true;
            //myRenderer.material = activatedMaterial;
        }
        //Get level numbers by parsing buttons name
        private void GetMyLevelNumbers()
        {
            //Int32.TryParse(myLevelName.Split('.')[0], out myLevelpackNumber);
            Int32.TryParse(myLevelName.Split('.')[1], out myLevelNumber);
        }

        private void SetMyLevelNumberText()
        {
            myText.text = myLevelNumber.ToString();
        }
    }
}