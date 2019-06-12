using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationManager : Manager
{
    public List<ElementData> elements = new List<ElementData>();
    public ElementInstance emptyElementPrefab;
    public AchievementData combinationsAchievement;
    protected override void SubscribeToDirector()
    {
        Director.SubscribeManager(this);
    }
    public bool CombinationExists(ElementInstance a, ElementInstance b)
    {
        ElementData validResult = null;
        foreach (ElementData element in elements)
        {
            if (element.IsFulfilledBy(a.Data, b.Data))
            {
                validResult = element;
                break;
            }
        }
        if (validResult != null)
        {
            ElementInstance element = Instantiate(emptyElementPrefab, (a.transform.position + b.transform.position) / 2, Quaternion.identity, a.transform.parent);

            element.Data = validResult;
            Director.GetManager<AchievementManager>().AddCount(validResult.creationAchievement);
            Director.GetManager<AchievementManager>().AddCount(combinationsAchievement);
        }

        return validResult != null;
    }
}
