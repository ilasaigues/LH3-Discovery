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
        OnBeginDrag += ItemPickedUp;
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
        Director.GetManager<InteractionManager>().SubscribeActiveInstance(this);
    }


    void ItemPickedUp()
    {
        if (Director.GetManager<AchievementManager>().GetCount(data.creationAchievement) <= 0)
        {
            Director.GetManager<AchievementManager>().AddCount(data.creationAchievement);
        }
    }

    void ItemDropped()
    {
        _elementsTouching.Sort((a, b) =>
        {
            if (a == null || b == null) return 0;
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
        if (collision.collider.isTrigger) return;

        ElementInstance element = collision.gameObject.GetComponent<ElementInstance>();

        if (element != null && !element.locked && Director.GetManager<CombinationManager>().CombinationExists(this, element))
        {
            Lock();
            Destroy(this.gameObject);
            Destroy(element.gameObject);
        }
    }

    private void Update()
    {

    }
}
