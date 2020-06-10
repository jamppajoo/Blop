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

        private LevelPackSelect levelPackSelect;

        //[HideInInspector]
        public int currentLevelPackNumber = 1;

        private void Awake()
        {
            levelButtons = gameObject.GetComponentsInChildren<LevelSelectButton>();
            levelPackSelect = FindObjectOfType<LevelPackSelect>();
        }
        private void SetLevelStars()
        {
            for (int i = 0; i < levelButtons.Length; i++)
            {
                //Get levelpack star amounts on 20 pieces chuck. 0-19 for first, 20-39 for second etc.
                levelButtons[i].SetStarAmount(GameManager.Instance.levelsStarAmount[(i + ((currentLevelPackNumber - 1) * (levelButtons.Length)))]);

            }
        }
        public void SetLevelPackNumber(int levelPackNumber)
        {
            currentLevelPackNumber = levelPackNumber;
            GameManager.Instance.levelPackNumber = currentLevelPackNumber;
            levelPackSelect.SetLevelPack(currentLevelPackNumber);
            for (int i = 0; i < levelButtons.Length; i++)
            {
                levelButtons[i].SetLevelPackNumber(currentLevelPackNumber);
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
