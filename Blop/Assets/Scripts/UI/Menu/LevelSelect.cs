using UnityEngine;

/// <summary>
/// Set up all star amounts to level select buttons. Starts scene when button is pressed
/// </summary>
/// 
namespace Menu
{
    public class LevelSelect : MonoBehaviour
    {
        private LevelSelectButton[] levelButtons;

        [HideInInspector]
        public int currentLevelPackNumber = 1;

        private void Awake()
        {
            levelButtons = gameObject.GetComponentsInChildren<LevelSelectButton>();
        }
        private void SetLevelStars()
        {
            for (int i = 0; i < levelButtons.Length; i++)
            {
                if ((levelButtons[i].myLevelNumber * levelButtons[i].myLevelPackNumber) - 1 == i)
                {
                    levelButtons[i].SetStarAmount(GameManager.Instance.levelsStarAmount[i]);
                }
            }
        }
        public void SetLevelPackNumber(int levelPackNumber)
        {
            currentLevelPackNumber = levelPackNumber;
            for (int i = 0; i < levelButtons.Length; i++)
            {
                levelButtons[i].SetLevelPackNumber(levelPackNumber);
            }
            SetLevelStars();
        }
        public void StartScene(string levelName)
        {
            GameManager.Instance.SmallVibrate();
            AnalyticsManager.Instance.SendLevelPassingAnalytics();
            GameManager.Instance.LoadLevel(levelName, true);
        }

    }
}
