using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Manager : MonoBehaviour
{
    protected void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SubscribeToDirector();
    }

    protected abstract void SubscribeToDirector();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
