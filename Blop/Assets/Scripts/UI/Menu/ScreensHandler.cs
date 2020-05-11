using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Menu
{
    public class ScreensHandler : MonoBehaviour
    {
        public MenuScreen mainScreen, levelPackSelectScreen, levelSelectScreen, settingsScreen;

        private LevelSelect levelSelect;

        private void Awake()
        {
            levelSelect = FindObjectOfType<LevelSelect>();
        }
        private void Start()
        {
            //If we come back from the game
            if (GameManager.Instance.timeSinceGameOpened > 1)
            {
                ShowLevelSelectScreen();
                levelSelect.SetLevelPackNumber(GameManager.Instance.levelPackNumber);
            }
            else
            {
                ShowMainScreen();
            }
        }
        public void ShowMainScreen()
        {
            DisappearAll();
            mainScreen.Show();
        }
        public void ShowLevelPackScreen()
        {
            DisappearAll();
            levelPackSelectScreen.Show();
        }
        public void ShowLevelSelectScreen()
        {
            DisappearAll();
            levelSelectScreen.Show();
        }
        public void ShowSettingsScreen()
        {
            DisappearAll();
            settingsScreen.Show();
        }

        private void DisappearAll()
        {
            mainScreen.Disappear();
            levelPackSelectScreen.Disappear();
            levelSelectScreen.Disappear();
            settingsScreen.Disappear();
        }

    }
}
