using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ScriptableValue<T> : ScriptableObject
{
    public T Value;
    public virtual T GetValue()
    {
        return Value;
    }
}
