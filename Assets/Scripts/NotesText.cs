using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesText : MonoBehaviour
{
    UnityEngine.UI.Text _text;
    SecretsManager _secretsManager;

    // Start is called before the first frame update
    void Start()
    {
        _secretsManager = Director.GetManager<SecretsManager>();
        _text = GetComponent<UnityEngine.UI.Text>();
        _secretsManager.OnSecretMouseEnter += HoverOver;
        _secretsManager.OnSecretMouseExit += Clear;
    }
    public void HoverOver(SecretData data)
    {

        _text.text = _secretsManager.IsDataUnlocked(data) ? data.revealedText : data.hint;
    }

    public void Clear(SecretData data)
    {
        _text.text = "";
    }
}
