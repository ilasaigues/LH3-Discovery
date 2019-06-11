using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesText : MonoBehaviour
{
    UnityEngine.UI.Text _text;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<UnityEngine.UI.Text>();
        Director.GetManager<SecretsManager>().OnSecretMouseEnter += HoverOver;
        Director.GetManager<SecretsManager>().OnSecretMouseExit += Clear;

    }
    public void HoverOver(SecretData data)
    {
        _text.text = data.hint;
    }

    public void Clear(SecretData data)
    {
        _text.text = "";
    }
}
