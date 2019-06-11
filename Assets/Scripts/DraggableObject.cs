using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DraggableObject : MonoBehaviour
{
    public DraggableConstraints constraints;

    protected bool _dragging = false;
    Vector3 _dragOffset = Vector3.zero;
    Rigidbody2D _rb2d;

    public System.Action OnBeginDrag = () => { };
    public System.Action OnEndDrag = () => { };
    public bool locked { get; private set; } = false;

    virtual protected void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    public void Lock(bool isLocked = true)
    {
        locked = isLocked;
    }

    virtual protected void FixedUpdate()
    {
        if (!locked && _dragging)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + _dragOffset;
            position.z = transform.position.z;
            if (constraints != null)
            {
                Vector3 center = constraints.transform.position;

                position = new Vector3(Mathf.Clamp(position.x, -center.x - constraints.size.x / 2, center.x + constraints.size.x / 2),
                                  Mathf.Clamp(position.y, -center.y - constraints.size.y / 2, center.y + constraints.size.y / 2), 0);
            }
            _rb2d.MovePosition(Vector3.Lerp(transform.position, position, .333333f));
        }
    }

    public void OnMouseDown()
    {
        if (!locked)
            _dragOffset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnMouseDrag()
    {
        if (!locked && !_dragging)
        {
            OnBeginDrag();
            _dragging = true;
        }
    }

    public void OnMouseUp()
    {
        if (!locked && _dragging)
        {
            OnEndDrag();
            _dragging = false;
        }

    }
}
