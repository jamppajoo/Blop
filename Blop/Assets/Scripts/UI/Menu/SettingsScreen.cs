using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class SettingsScreen : MenuScreen
    {
        public Button backButton;


        private void Start()
        {
            backButton.onClick.AddListener(Back);
        }

        private void Back()
        {
            screensHandler.ShowMainScreen();
        }
    }
}
