using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindInteraction : Interaction
{
    public float pushPower = 1;
    public ElementData airData;
    private ElementInstance _airInstance;
    private ElementInstance _powerInstance;

    public override void Interact(ElementInstance element)
    {
        if (element.dragging) return;
        element.transform.position += transform.up * pushPower * Time.deltaTime;
    }
    protected override void Reposition()
    {
        if (a.Data == airData)
        {
            _airInstance = a;
            _powerInstance = b;
        }
        else
        {
            _airInstance = b;
            _powerInstance = a;
        }

        transform.position = _airInstance.transform.position;
    }

    protected override void Resize()
    {
        transform.localScale = new Vector3(1, data.distance - Vector3.Distance(a.transform.position, b.transform.position), 1);
        //transform.localScale = new Vector2((Vector3.Distance(a.transform.position, transform.position) * 2 - .5f), 1.5f);
    }

    protected override void Redirect()
    {
        transform.up = _airInstance.transform.position - _powerInstance.transform.position;
    }
}
