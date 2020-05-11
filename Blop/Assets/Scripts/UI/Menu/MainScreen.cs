using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class MainScreen : MenuScreen
    {
        public Button playButton, settingsButton;

        protected override void Awake()
        {
            base.Awake();
            originalPosition = new Vector3(0, 2500, 0);
            Debug.Log("Main screen awake");
        }
        private void Start()
        {
            playButton.onClick.AddListener(Play);
            settingsButton.onClick.AddListener(Settings);
        }

        private void Play()
        {
            screensHandler.ShowLevelPackScreen();
        }
        private void Settings()
        {
            screensHandler.ShowSettingsScreen();
        }
    }
}
