using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelStats", menuName = "ScriptableObjects/LevelStatsScriptableObject", order = 1)]
public class LevelStats : ScriptableObject
{
    public int threeStarMovementAmount;
    public int twoStarMovementAmount;
}
