using UnityEngine;

/// <summary>
/// ScriptableObject to handle level max button presses amount to achieve certain starAmounts
/// </summary>
[CreateAssetMenu(fileName = "LevelStats", menuName = "ScriptableObjects/LevelStatsScriptableObject", order = 1)]
public class LevelStats : ScriptableObject
{
    public int threeStarMovementAmount;
    public int twoStarMovementAmount;
}
