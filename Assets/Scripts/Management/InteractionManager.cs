using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : Manager
{
    public List<InteractionData> availableInteractions = new List<InteractionData>();
    protected override void SubscribeToDirector()
    {
        Director.SubscribeManager(this);
    }

    private Dictionary<int, Interaction> activeInteractions = new Dictionary<int, Interaction>();


    public Interaction GetInteraction(ElementInstance a, ElementInstance b)
    {
        int hash = a.GetHashCode() + b.GetHashCode();
        if (activeInteractions.ContainsKey(hash)) return activeInteractions[hash];
        InteractionData validInteraction = null;
        foreach (var interaction in availableInteractions)
        {
            if (interaction.IsFulfilledBy(a.data, b.data))
            {
                validInteraction = interaction;
            }
        }
        if (validInteraction != null)
        {
            Interaction newInteraction = Instantiate(validInteraction.interactionPrefab);

            newInteraction.a = a;
            newInteraction.b = b;
            newInteraction.transform.position = (a.transform.position + b.transform.position) / 2;
            activeInteractions[hash] = newInteraction;
            return newInteraction;
        }
        return null;
    }

    public void CancelInteraction(ElementInstance a, ElementInstance b)
    {
        int hash = a.GetHashCode() + b.GetHashCode();

        if (activeInteractions.ContainsKey(hash))
        {
            activeInteractions[hash].Kill();
            activeInteractions.Remove(hash);
        }
    }
}
