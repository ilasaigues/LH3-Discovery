using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
public class SecretsManager : Manager
{

    public System.Action<SecretData> OnSecretMouseEnter = (s) => { };
    public System.Action<SecretData> OnSecretMouseExit = (s) => { };

    protected override void SubscribeToDirector()
    {
        Director.SubscribeManager(this);
    }

    public List<SecretData> lockedSecrets = new List<SecretData>();
    public List<SecretData> unlockedSecrets = new List<SecretData>();

    private void Start()
    {
        Director.GetManager<AchievementManager>().achievementCountUpdated += CheckAllSecrets;
    }

    void CheckAllSecrets()
    {
        for (int i = lockedSecrets.Count - 1; i >= 0; i--)
        {
            var secret = lockedSecrets[i];

            bool fulfilled = true;
            foreach (var condition in secret.unlockConditions)
            {
                fulfilled &= Director.GetManager<AchievementManager>().GetCount(condition.data) >= condition.count;
            }
            if (fulfilled)
            {
                unlockedSecrets.Add(secret);
                lockedSecrets.RemoveAt(i);
            }
        }
    }

    public SecretData GetSecretData(string word)
    {
        word = Regex.Replace(word, "[^A-Za-z0-9 _]", "");
        foreach (var secret in lockedSecrets)
        {
            foreach (var variation in secret.words)
            {
                if (word.ToLower().Equals(variation.ToLower())) return secret;
            }
        }
        foreach (var secret in unlockedSecrets)
        {
            foreach (var variation in secret.words)
            {
                if (word.ToLower().Equals(variation.ToLower())) return secret;
            }
        }
        return null;
    }

    public bool IsDataUnlocked(SecretData data)
    {
        return unlockedSecrets.Contains(data);
    }
}
