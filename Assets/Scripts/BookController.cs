using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BookController : MonoBehaviour
{
    public List<BookPage> pages = new List<BookPage>();
    public Button prevPageButton;
    public Button nextPageButton;
    public Text notesText;
    int _currentPage = 0;

    public System.Action<SecretData> OnSecretClicked = (s) => { };

    // Start is called before the first frame update
    void Start()
    {
        foreach (BookPage page in pages)
        {
            page.OnSecretMouseEnter += DisplaySecretHint;
            page.OnSecretMouseExit += ClearHint;
            page.OnSecretClicked += OnSecretClicked;
        }
    }

    void DisplaySecretHint(SecretData data)
    {
        notesText.text = data.hint;
    }

    void ClearHint(SecretData data)
    {
        notesText.text = "";
    }

    public void NextPage()
    {
        int _oldPage = _currentPage;
        _currentPage = Mathf.Clamp(_currentPage + 1, 0, pages.Count - 1);
        if (_currentPage != _oldPage)
        {
            pages[_oldPage].gameObject.SetActive(false);
            pages[_currentPage].gameObject.SetActive(true);

        }
    }

    public void PrevPage()
    {
        int _oldPage = _currentPage;
        _currentPage = Mathf.Clamp(_currentPage - 1, 0, pages.Count - 1);
        if (_currentPage != _oldPage)
        {
            pages[_oldPage].gameObject.SetActive(false);
            pages[_currentPage].gameObject.SetActive(true);
        }
    }



    // Update is called once per frame
    void Update()
    {
        prevPageButton.interactable = _currentPage > 0;
        nextPageButton.interactable = _currentPage < pages.Count - 1; //&& pages[_currentPage].Completed();
    }
}
