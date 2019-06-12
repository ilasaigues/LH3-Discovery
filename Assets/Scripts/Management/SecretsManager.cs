using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
public class SecretsManager : Manager
{
    public System.Action<SecretData, DiscoveryData> OnDiscoveryAssigned = (s, d) => { };
    protected override void SubscribeToDirector()
    {
        Director.SubscribeManager(this);
    }
    public List<SecretData> allSecrets = new List<SecretData>();
    public Dictionary<SecretData, DiscoveryData> matches = new Dictionary<SecretData, DiscoveryData>();

    private void Start()
    {
        foreach (var secret in allSecrets)
        {
            matches[secret] = null;
        }
    }

    public SecretData GetSecretData(string word)
    {
        word = Regex.Replace(word, "[^A-Za-z0-9 _]", "");
        foreach (var secret in allSecrets)
        {
            foreach (var variation in secret.words)
            {
                if (word.ToLower().Equals(variation.ToLower())) return secret;
            }
        }
        return null;
    }

    public DiscoveryData GetAssignedDiscovery(SecretData secret)
    {
        return matches[secret];
    }

    public void AssignDiscoveryToData(SecretData secret, DiscoveryData discovery)
    {
        matches[secret] = discovery;
        OnDiscoveryAssigned(secret, discovery);
    }

}
