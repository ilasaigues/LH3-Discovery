using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Element", menuName = "Scriptables/Element")]
public class ElementData : RecipeData
{
    public Sprite sprite;
    public SoundValue creationSound;
    public AchievementData creationAchievement;
}
