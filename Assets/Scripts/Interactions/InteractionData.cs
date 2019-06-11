using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newInteractionData", menuName = "Scriptables/Interaction Data")]
public class InteractionData : RecipeData
{
    public float distance = 5;
    public Interaction interactionPrefab;
}
