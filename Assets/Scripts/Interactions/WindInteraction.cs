using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindInteraction : Interaction
{
    public float pushPower = 1;

    public override void OnTriggerEnter2D(Collider2D other)
    {
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.isTrigger) return;
        ElementInstance element = other.GetComponent<ElementInstance>();
        if (element != null && element != a && element != b && element.data != a.data) PushElement(element);
    }

    void PushElement(ElementInstance element)
    {
        if (element.dragging) return;
        Debug.Log("Push");
        Vector3 center = transform.position;
        Vector3 side = (a.transform.position - center).normalized;
        Vector3 pushDirection = Vector3.Cross(Vector3.forward, side);
        element.transform.position += pushDirection * pushPower * Time.deltaTime;
    }

    protected override void Resize()
    {
        transform.localScale = new Vector2((Vector3.Distance(a.transform.position, transform.position) * 2 - .5f), 1.5f);
    }
}
