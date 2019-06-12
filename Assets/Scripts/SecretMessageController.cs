using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SecretMessageController : MonoBehaviour
{
    public Text regularText;
    public Text codedText;
    Queue<DiscoveryData> _queuedSecrets = new Queue<DiscoveryData>();
    bool _displayingMessage;
    Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        Director.GetManager<DiscoveryManager>().OnNewDiscovery += EnqueueDiscovery;
        _animator = GetComponent<Animator>();
    }

    void EnqueueDiscovery(DiscoveryData discovery)
    {
        _queuedSecrets.Enqueue(discovery);
    }

    public void AnimationOver()
    {
        _displayingMessage = false;
        _animator.SetBool("ShowingMessage", false);
    }

    private void Update()
    {
        if (!_displayingMessage)
        {
            if (_queuedSecrets.Count > 0)
            {
                _displayingMessage = true;
                DiscoveryData discovery = _queuedSecrets.Dequeue();
                Debug.Log("Displaying discovery: " + discovery.name);
                regularText.text = codedText.text = discovery.name;
                _animator.SetBool("ShowingMessage", true);
            }
        }
    }

}
