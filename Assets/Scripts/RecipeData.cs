using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RecipeData : ScriptableObject
{
    public ElementData componentA;
    public ElementData componentB;

    public bool IsFulfilledBy(ElementData a, ElementData b)
    {
        if (componentA == null || componentB == null) return false;
        return (this.componentA == a && this.componentB == b) || (this.componentA == b && this.componentB == a);

    }
}
