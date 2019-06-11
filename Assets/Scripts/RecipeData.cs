using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RecipeData : ScriptableObject
{
    public ElementData a;
    public ElementData b;

    public bool IsFulfilledBy(ElementData a, ElementData b)
    {
        return (this.a == a && this.b == b) || (this.a == b && this.b == a);

    }
}
