using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SecretDisplay : MonoBehaviour
{
    public SecretData secret;
    public Text codedText;
    public Text revealedText;

    public System.Action<SecretData> OnPointerEnter = (s) => { };
    public System.Action<SecretData> OnPointerExit = (s) => { };
    public System.Action<SecretData> OnClick = (s) => { };

    public void OnMouseEnter()
    {
        if (secret != null)
        {
            OnPointerEnter(secret);
        }
    }

    public void OnMouseExit()
    {
        if (secret != null)
        {
            OnPointerExit(secret);
        }
    }

    public void OnMouseDown()
    {
        OnClick(secret);
    }

    public void Initialize(SecretData data, string word)
    {
        Director.GetManager<SecretsManager>().OnDiscoveryAssigned += UpdateDisplay;
        this.secret = data;
        codedText.text = revealedText.text = word;
        CheckDisplayUnlock();
    }

    void UpdateDisplay(SecretData secret, DiscoveryData discoveryData)
    {
        if (secret == this.secret) CheckDisplayUnlock();
    }

    void CheckDisplayUnlock()
    {
        if (Director.GetManager<SecretsManager>().GetAssignedDiscovery(secret))
        {
            if (codedText != null)
            {
                var col = codedText.color;
                col.a = .15f;
                codedText.color = col;
            }
            if (revealedText != null)
            {
                revealedText.gameObject.SetActive(true);
                revealedText.text = Director.GetManager<SecretsManager>().GetAssignedDiscovery(secret).name;
            }
        }
        else
        {
            var col = codedText.color;
            col.a = 1;
            codedText.color = col;
            if (revealedText != null)
            {
                revealedText.gameObject.SetActive(false);
            }
        }
    }
}
