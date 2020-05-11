using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class LevelPackSelectScreen : MenuScreen
    {
        public Button backButton;

        public Button levelPack1Button, levelPack2Button, levelPack3Button;

        private LevelSelect levelSelect;

        protected override void Awake()
        {
            base.Awake();
            levelSelect = FindObjectOfType<LevelSelect>();
        }

        private void Start()
        {
            backButton.onClick.AddListener(Back);
            levelPack1Button.onClick.AddListener(delegate { OpenLevelPack(1); });
            levelPack2Button.onClick.AddListener(delegate { OpenLevelPack(2); });
            levelPack3Button.onClick.AddListener(delegate { OpenLevelPack(3); });

            levelPack1Button.GetComponentInChildren<Text>().text = GameManager.Instance.LevelPackNames[0];
            levelPack2Button.GetComponentInChildren<Text>().text = GameManager.Instance.LevelPackNames[1];
            levelPack3Button.GetComponentInChildren<Text>().text = GameManager.Instance.LevelPackNames[2];
        }
        private void OpenLevelPack(int levelPackNumber)
        {
            levelSelect.SetLevelPackNumber(levelPackNumber);
            screensHandler.ShowLevelSelectScreen();
        }
        private void Back()
        {
            screensHandler.ShowMainScreen();
        }
    }
}
