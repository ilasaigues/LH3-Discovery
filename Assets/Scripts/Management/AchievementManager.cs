using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : Manager
{
    [System.Serializable]
    public class AchievementCounter
    {
        public AchievementData data;
        public int count;
    }

    protected override void SubscribeToDirector()
    {
        Director.SubscribeManager(this);
    }

    public AchievementData pickupAchievement;
    public AchievementData interactionAchievement;

    public System.Action achievementCountUpdated = () => { };

    public Dictionary<AchievementData, int> achievementCount = new Dictionary<AchievementData, int>();

    public void AddCount(AchievementData data)
    {
        if (data == null)
        {
            Debug.Log("Null in achievement data");
            return;
        }
        if (achievementCount.ContainsKey(data))
        {
            achievementCount[data]++;
        }
        else achievementCount[data] = 1;
        achievementCountUpdated();
    }

    public int GetCount(AchievementData data)
    {
        if (data == null)
        {
            Debug.Log("Null in achievement data");
            return 0;
        }
        if (achievementCount.ContainsKey(data)) return achievementCount[data];
        return 0;
    }
}
