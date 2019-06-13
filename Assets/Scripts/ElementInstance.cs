using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(SpriteRenderer))]
public class ElementInstance : DraggableObject
{
    public SoundValue pickupSound;
    public SoundValue dropSound;

    private ElementData _data;
    public ElementData Data
    {
        get { return _data; }
        set
        {
            if (_data != value)
            {
                _data = value;
                Initialize();
            }
        }
    }
    private SpriteRenderer _spriteRenderer;

    private List<ElementInstance> _elementsTouching = new List<ElementInstance>();
    private List<ElementInstance> _elementsInteracting = new List<ElementInstance>();


    private void Start()
    {
        OnBeginDrag += ItemPickedUp;
        OnEndDrag += ItemDropped;

        Initialize();
        Director.GetManager<InteractionManager>().SubscribeActiveInstance(this);
    }


    void Initialize()
    {
        if (_spriteRenderer == null)
            _spriteRenderer = GetComponent<SpriteRenderer>();
        if (Data != null)
        {
            gameObject.name = Data.name;
            _spriteRenderer.sprite = Data.sprite;
            if (Data.creationSound != null) Director.GetManager<SoundManager>().PlaySound(Data.creationSound);
        }
        else
        {
            Debug.LogWarning("Element instance was provided with no data. Destroying");
            Destroy(gameObject);
        }
    }

    void ItemPickedUp()
    {
        AchievementManager achManager = Director.GetManager<AchievementManager>();
        if (achManager.GetCount(Data.creationAchievement) <= 0)
        {
            achManager.AddCount(Data.creationAchievement);
        }
        achManager.AddCount(achManager.pickupAchievement);
        Director.GetManager<SoundManager>().PlaySound(pickupSound);
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
        Director.GetManager<SoundManager>().PlaySound(dropSound);
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
