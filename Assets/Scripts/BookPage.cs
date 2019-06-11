using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BookPage : MonoBehaviour
{
    [TextArea]
    public string text;
    public Text emtpyWordPrefab;
    public Text emtpyWordCodedPrefab;

    public float lineHeight = 25;
    public Vector2 margin;
    private List<List<Text>> _paragraphs = new List<List<Text>>();
    // Start is called before the first frame update
    void OnEnable()
    {
        string[] splitlines = text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        foreach (var paragraphString in splitlines)
        {
            List<Text> thisParagraph = new List<Text>();
            foreach (var word in paragraphString.Split(' '))
            {
                SecretData secret = Director.GetManager<SecretsManager>().GetSecretData(word);
                Text currentWord = null;
                if (secret != null)
                {
                    currentWord = Instantiate(emtpyWordCodedPrefab, transform);

                    currentWord.GetComponent<SecretDisplay>().Initialize(secret, word, Director.GetManager<SecretsManager>().IsDataUnlocked(secret));
                }
                else
                {
                    currentWord = Instantiate(emtpyWordPrefab, transform);
                }
                currentWord.text = word + " ";
                thisParagraph.Add(currentWord);
            }
            _paragraphs.Add(thisParagraph);
        }
    }

    private void OnDisable()
    {
        foreach (var paragraph in _paragraphs)
        {
            foreach (var word in paragraph)
            {
                Destroy(word.gameObject);
            }
        }
        _paragraphs.Clear();
    }
    // Update is called once per frame
    void Update()
    {
        float currentLine = 0;


        foreach (var paragraph in _paragraphs)
        {

            float currentLineWidth = 5;
            foreach (var word in paragraph)
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
            currentLine += 1.5f;
        }
    }
}
