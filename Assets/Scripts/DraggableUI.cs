using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public abstract class DraggableUI : MonoBehaviour, IPointerDownHandler,IPointerUpHandler
{

    public System.Action<DraggableUI> OnPickup = (d) => { };
    public System.Action<DraggableUI> OnDrop = (d) => { };

    public abstract void OnPointerDown(PointerEventData eventData);

    public abstract void OnPointerUp(PointerEventData eventData);
}
