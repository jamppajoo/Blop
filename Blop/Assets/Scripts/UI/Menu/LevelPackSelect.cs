using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class LevelPackSelect : MonoBehaviour
    {
        public Button previousLevelPackButton, nextLevelPackButton;

        public Text levelPackNameText;
        public ScrollRect levelsScrollRect;

        private LevelSelect levelSelect;

        private void Awake()
        {
            levelSelect = FindObjectOfType<LevelSelect>();
            previousLevelPackButton.onClick.AddListener(PreviousLevelPack);
            nextLevelPackButton.onClick.AddListener(NextLevelPack);
        }
        private void PreviousLevelPack()
        {
            levelsScrollRect.verticalNormalizedPosition = 1;
            levelSelect.SetLevelPackNumber(GameManager.Instance.levelPackNumber - 1);
        }
        private void NextLevelPack()
        {
            levelsScrollRect.verticalNormalizedPosition = 1;
            levelSelect.SetLevelPackNumber(GameManager.Instance.levelPackNumber + 1);
        }
        public void SetLevelPack(int index)
        {
            levelPackNameText.text = GameManager.Instance.LevelPackNames[index - 1];
            previousLevelPackButton.gameObject.SetActive(true);
            nextLevelPackButton.gameObject.SetActive(true);
            if (index == 1)
            {
                previousLevelPackButton.gameObject.SetActive(false);
            }
            if (index == 3)
            {
                nextLevelPackButton.gameObject.SetActive(false);
            }
        }
    }
}
