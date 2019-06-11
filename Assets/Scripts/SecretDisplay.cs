using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SecretDisplay : MonoBehaviour
{
    public SecretData data;
    public Text codedText;
    public Text revealedText;
    public void OnMouseEnter()
    {
        if (data != null)
        {
            Director.GetManager<SecretsManager>().OnSecretMouseEnter(data);
        }
    }

    public void OnMouseExit()
    {
        if (data != null)
        {
            Director.GetManager<SecretsManager>().OnSecretMouseExit(data);
        }
    }

    public void Initialize(SecretData data, bool reveal)
    {
        this.data = data;
        codedText.text = revealedText.text = data.word;
        if (reveal)
        {
            var col = codedText.color;
            col.a = .15f;
            codedText.color = col;
            revealedText.gameObject.SetActive(true);
        }
    }
}
