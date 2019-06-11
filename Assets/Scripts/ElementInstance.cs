using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(SpriteRenderer))]
public class ElementInstance : DraggableObject
{
    public ElementData data;
    private SpriteRenderer _spriteRenderer;

    private List<ElementInstance> _elementsTouching = new List<ElementInstance>();
    private List<ElementInstance> _elementsInteracting = new List<ElementInstance>();


    private void Start()
    {
        OnEndDrag += ItemDropped;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (data != null)
        {
            gameObject.name = data.name;
            _spriteRenderer.sprite = data.sprite;
        }
        else
        {
            Debug.LogWarning("Element instance was provided with no data. Destroying");
            Destroy(gameObject);
        }
    }




    void ItemDropped()
    {
        _elementsTouching.Sort((a, b) =>
        {
            if (Vector3.Distance(a.transform.position, transform.position) > Vector3.Distance(b.transform.position, transform.position))
                return 1;
            return -1;
        });
        foreach (var element in _elementsTouching)
        {
            if (Director.GetManager<CombinationManager>().CombinationExists(this, element))
            {
                Destroy(this.gameObject);
                Destroy(element.gameObject);
                break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_dragging)
        {
            ElementInstance element = collision.gameObject.GetComponent<ElementInstance>();
            if (element != null && !element.locked)
            {
                if (!_elementsTouching.Contains(element)) _elementsTouching.Add(element);
                if (_elementsInteracting.Contains(element))
                {
                    _elementsInteracting.Remove(element);
                    Director.GetManager<InteractionManager>().CancelInteraction(this, element);
                }

            }
        }
    }

    private void Update()
    {
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (_dragging)
        {
            ElementInstance element = collision.gameObject.GetComponent<ElementInstance>();
            if (element != null && !element.locked)
            {
                if (_elementsTouching.Contains(element)) _elementsTouching.Remove(element);
                if (!_elementsInteracting.Contains(element))
                {
                    _elementsInteracting.Add(element);
                    Director.GetManager<InteractionManager>().GetInteraction(this, element);

                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        ElementInstance element = collision.gameObject.GetComponent<ElementInstance>();
        if (element != null && !element.locked)
        {
            if (!_elementsInteracting.Contains(element))
            {
                _elementsInteracting.Add(element);
                Director.GetManager<InteractionManager>().GetInteraction(this, element);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ElementInstance element = collision.gameObject.GetComponent<ElementInstance>();
        if (element != null && !element.locked)
        {
            if (_elementsInteracting.Contains(element))
            {
                _elementsInteracting.Remove(element);
                Director.GetManager<InteractionManager>().CancelInteraction(this, element);
            }
        }
    }
}
