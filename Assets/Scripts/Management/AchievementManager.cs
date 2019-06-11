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

    public System.Action achievementCountUpdated = () => { };

    public Dictionary<AchievementData, int> achievementCount = new Dictionary<AchievementData, int>();

    public void AddCount(AchievementData data)
    {
        if (achievementCount.ContainsKey(data)) achievementCount[data]++;
        else achievementCount[data] = 1;
        achievementCountUpdated();
    }

    public int GetCount(AchievementData data)
    {
        if (achievementCount.ContainsKey(data)) return achievementCount[data];
        return 0;
    }
}
