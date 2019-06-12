using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newSecretData", menuName = "Scriptables/Secret")]
public class SecretData : ScriptableObject
{
    public List<string> words = new List<string>();
    [TextArea]
    public string hint;
}
