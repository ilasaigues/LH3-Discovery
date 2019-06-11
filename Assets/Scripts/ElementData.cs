using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Element", menuName = "Scriptables/Element")]
public class ElementData : ScriptableObject
{
    public Sprite sprite;
    public AchievementData creationAchievement;
}
