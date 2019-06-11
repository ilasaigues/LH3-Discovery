using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DraggableObject : MonoBehaviour
{

    public bool dragging { get; private set; } = false;
    Vector3 _dragOffset = Vector3.zero;
    Rigidbody2D _rb2d;



    public System.Action OnBeginDrag = () => { };
    public System.Action OnEndDrag = () => { };
    public bool locked { get; private set; } = false;
    int _originalLayer;

    virtual protected void Awake()
    {
        _originalLayer = gameObject.layer;
        _rb2d = GetComponent<Rigidbody2D>();
    }

    public void Lock(bool isLocked = true)
    {
        locked = isLocked;
    }

    virtual protected void FixedUpdate()
    {
        if (!locked && dragging)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + _dragOffset;
            position.z = transform.position.z;
            _rb2d.AddForce((position - transform.position), ForceMode2D.Impulse);
        }
    }

    public void OnMouseDown()
    {
        if (!locked)
            _dragOffset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnMouseDrag()
    {
        if (!locked && !dragging)
        {
            OnBeginDrag();
            gameObject.layer = 10;
            dragging = true;
        }
    }

    public void OnMouseUp()
    {
        if (!locked && dragging)
        {
            OnEndDrag();
            gameObject.layer = _originalLayer;
            dragging = false;
        }

    }
}
