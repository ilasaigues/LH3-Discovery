using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction : MonoBehaviour
{

    public ElementInstance a;
    public ElementInstance b;

    public InteractionData data;

    public System.Action OnKill = () => { };

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

    public virtual void Kill()
    {
        OnKill();
        Destroy(gameObject);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (a == null || b == null || Vector3.Distance(a.transform.position, b.transform.position) > data.distance)
        {
            Kill();
            return;
        }
        transform.position = (a.transform.position + b.transform.position) / 2;
        Resize();
        transform.right = a.transform.position - transform.position;
    }



    protected virtual void Resize()
    {
        transform.localScale = Vector3.one * (Vector3.Distance(a.transform.position, transform.position) * 2 - .5f);
    }

    public abstract void OnTriggerEnter2D(Collider2D other);
}
