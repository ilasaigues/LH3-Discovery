using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoveryManager : Manager
{
    public System.Action<DiscoveryData> OnNewDiscovery = (d) => { };

    protected override void SubscribeToDirector()
    {
        Director.SubscribeManager(this);
    }


    public List<DiscoveryData> lockedDiscoveries = new List<DiscoveryData>();
    public List<DiscoveryData> unlockedDiscoveries = new List<DiscoveryData>();

    private void Start()
    {
        Director.GetManager<AchievementManager>().achievementCountUpdated += CheckAllDiscoveries;
    }

    void CheckAllDiscoveries()
    {
        for (int i = lockedDiscoveries.Count - 1; i >= 0; i--)
        {
            var secret = lockedDiscoveries[i];

            bool fulfilled = true;
            foreach (var condition in secret.unlockConditions)
            {
                fulfilled &= Director.GetManager<AchievementManager>().GetCount(condition.data) >= condition.count;
            }
            if (fulfilled)
            {
                OnNewDiscovery(secret);
                unlockedDiscoveries.Add(secret);
                lockedDiscoveries.RemoveAt(i);
            }
        }
    }

    public bool IsDataUnlocked(DiscoveryData data)
    {
        return lockedDiscoveries.Contains(data);
    }
}
