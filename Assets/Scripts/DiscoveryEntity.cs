using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DiscoveryEntity : DraggableUI
{
    public Text text;
    public Image imageDrawer;

    private DiscoveryData _discoveryData;
    public DiscoveryData discoveryData
    {
        get { return _discoveryData; }
        set
        {
            _discoveryData = value;
            text.text = value.name;
            imageDrawer.sprite = value.sprite;
            imageDrawer.color = value.sprite != null ? Color.white : new Color(0, 0, 0, 0);
        }
    }



    bool _dragging;
    public override void OnPointerDown(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        _dragging = true;
        OnPickup(this);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        _dragging = false;
        OnDrop(this);
    }

    private void Update()
    {
        if (_dragging)
            transform.position = Vector3.Lerp(transform.position, Input.mousePosition, 1f / 3);
    }
}
