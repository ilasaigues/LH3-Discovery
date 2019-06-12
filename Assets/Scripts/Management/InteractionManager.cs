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



    private Dictionary<int, Interaction> _activeInteractions = new Dictionary<int, Interaction>();
    private List<ElementData> _interactibleData = new List<ElementData>();
    private List<ElementInstance> _activeInstances = new List<ElementInstance>();


    private void Start()
    {
        foreach (var interaction in availableInteractions)
        {
            if (!_interactibleData.Contains(interaction.componentA)) _interactibleData.Add(interaction.componentA);
            if (!_interactibleData.Contains(interaction.componentB)) _interactibleData.Add(interaction.componentB);
        }
    }

    public void SubscribeActiveInstance(ElementInstance element)
    {
        if (_interactibleData.Contains(element.Data))
        {
            _activeInstances.Add(element);
        }
    }

    public void Update()
    {
        for (int i = 0; i < _activeInstances.Count; i++)
        {
            if (_activeInstances[i] == null) _activeInstances.RemoveAt(i);
            else foreach (var b in _activeInstances)
                {
                    if (_activeInstances[i] == b) continue;
                    GetInteraction(_activeInstances[i], b);
                }

        }
    }

    public Interaction GetInteraction(ElementInstance a, ElementInstance b)
    {
        if (a == null || b == null) return null;
        int hash = a.GetHashCode() + b.GetHashCode();
        if (_activeInteractions.ContainsKey(hash)) return _activeInteractions[hash];
        InteractionData validInteraction = null;
        foreach (var interaction in availableInteractions)
        {
            if (interaction.IsFulfilledBy(a.Data, b.Data))
            {
                if (Vector3.Distance(a.transform.position, b.transform.position) < interaction.distance)
                    validInteraction = interaction;
                else
                    CancelInteraction(a, b);
            }
        }
        if (validInteraction != null)
        {
            Interaction newInteraction = Instantiate(validInteraction.interactionPrefab);
            newInteraction.Birth(a, b);            
            newInteraction.OnKill += () => { _activeInteractions.Remove(hash); };
            newInteraction.transform.position = (a.transform.position + b.transform.position) / 2;
            _activeInteractions[hash] = newInteraction;
            return newInteraction;
        }
        return null;
    }

    public void CancelInteraction(ElementInstance a, ElementInstance b)
    {
        int hash = a.GetHashCode() + b.GetHashCode();

        if (_activeInteractions.ContainsKey(hash))
        {
            _activeInteractions[hash].Kill();
        }
    }


}
