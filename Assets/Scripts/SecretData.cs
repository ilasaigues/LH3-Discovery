using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newSecretData", menuName = "Scriptables/Secret")]
public class SecretData : ScriptableObject
{
    public string word;
    public string hint;

    public List<AchievementManager.AchievementCounter> unlockConditions = new List<AchievementManager.AchievementCounter>();
}
