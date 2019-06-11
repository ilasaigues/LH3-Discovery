using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BookPage : MonoBehaviour
{
    public string text;
    public Text emtpyWordPrefab;
    public Text emtpyWordCodedPrefab;

    public float lineHeight = 25;
    public Vector2 margin;
    private List<Text> _words = new List<Text>();

    // Start is called before the first frame update
    void OnEnable()
    {
        Debug.Log("Enable");
        text = "This is a guide for the beginner alchemist. The secrets contained in this book are not for the eyes of mortals of feeble minds.";

        foreach (var word in text.Split(' '))
        {
            SecretData secret = Director.GetManager<SecretsManager>().GetSecretData(word);
            Text currentWord = null;
            if (secret != null)
            {
                currentWord = Instantiate(emtpyWordCodedPrefab, transform);

                currentWord.GetComponent<SecretDisplay>().Initialize(secret, Director.GetManager<SecretsManager>().IsDataUnlocked(secret));
            }
            else
            {
                currentWord = Instantiate(emtpyWordPrefab, transform);
            }
            currentWord.text = word + " ";
            _words.Add(currentWord);
        }
    }

    private void OnDisable()
    {
        foreach (var item in _words)
        {
            Destroy(item.gameObject);
        }
        _words.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        int currentLine = 0;

        float currentLineWidth = 0;
        foreach (var word in _words)
        {
            float wordWidth = word.rectTransform.rect.width;

            if (currentLineWidth + wordWidth > GetComponent<RectTransform>().rect.width - margin.x * 2)
            {
                currentLine++;
                currentLineWidth = wordWidth;
            }
            else
            {
                currentLineWidth += wordWidth;
            }
            word.transform.localPosition = new Vector2(currentLineWidth - wordWidth, -currentLine * lineHeight)
                - new Vector2(GetComponent<RectTransform>().rect.width, -GetComponent<RectTransform>().rect.height) / 2
                + new Vector2(margin.x, -margin.y);
        }
    }
}
