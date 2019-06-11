using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction : MonoBehaviour
{

    public ElementInstance a;
    public ElementInstance b;

    CircleCollider2D _collider;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
    }

    public void Birth(ElementInstance a, ElementInstance b)
    {
        this.a = a;
        this.b = b;
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (a == null || b == null)
        {
            Destroy(this.gameObject);
            return;
        }
        transform.position = (a.transform.position + b.transform.position) / 2;
        transform.localScale = Vector3.one * (Vector3.Distance(a.transform.position, transform.position) * 2 - .5f);
        transform.right = a.transform.position - transform.position;
    }

    public abstract void OnTriggerEnter2D(Collider2D other);
}
