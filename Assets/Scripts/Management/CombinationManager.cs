using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationManager : Manager
{
    public List<CombinationData> combinations = new List<CombinationData>();
    public ElementInstance emptyElementPrefab;
    public AchievementData combinationsAchievement;

    protected override void SubscribeToDirector()
    {
        Director.SubscribeManager(this);
    }
    public bool CombinationExists(ElementInstance a, ElementInstance b)
    {

        CombinationData validCombo = null;
        foreach (CombinationData combo in combinations)
        {
            if (combo.IsFulfilledBy(a.data, b.data))
            {
                validCombo = combo;
                break;
            }
        }
        if (validCombo != null && validCombo.result != null)
        {
            Instantiate(emptyElementPrefab, (a.transform.position + b.transform.position) / 2, Quaternion.identity, a.transform.parent).data = validCombo.result;
            Director.GetManager<AchievementManager>().AddCount(combinationsAchievement);
        }

        return validCombo != null;
    }
}
