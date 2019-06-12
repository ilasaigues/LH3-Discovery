using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newDiscovery", menuName = "Scriptables/Discovery")]
public class DiscoveryData : ScriptableObject
{
    public Sprite sprite;
    public List<AchievementManager.AchievementCounter> unlockConditions = new List<AchievementManager.AchievementCounter>();
}
