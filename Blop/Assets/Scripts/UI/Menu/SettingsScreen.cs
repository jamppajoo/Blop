using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class SettingsScreen : MenuScreen
    {
        public Button backButton, creditsButton;


        private void Start()
        {
            backButton.onClick.AddListener(Back);
            creditsButton.onClick.AddListener(ShowCredits);
        }

        private void ShowCredits()
        {
            screensHandler.ShowCreditsScreen();
        }

        private void Back()
        {
            screensHandler.ShowMainScreen();
        }
    }
}
