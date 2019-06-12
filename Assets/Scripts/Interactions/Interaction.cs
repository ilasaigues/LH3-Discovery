using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction : MonoBehaviour
{

    public ElementInstance a;
    public ElementInstance b;

    public InteractionData data;
    public AchievementData interactionAchievement;
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
        InteractionAchievement();
    }

    private  void InteractionAchievement()
    {
        AchievementManager achManager = Director.GetManager<AchievementManager>();
        achManager.AddCount(interactionAchievement);
        achManager.AddCount(achManager.interactionAchievement);
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
        Reposition();
        Resize();
        Redirect();
    }

    protected virtual void Reposition()
    {
        transform.position = (a.transform.position + b.transform.position) / 2;
    }

    protected virtual void Resize()
    {
        transform.localScale = Vector3.one * (Vector3.Distance(a.transform.position, transform.position) * 2 - .5f);
    }
    protected virtual void Redirect()
    {
        transform.right = a.transform.position - transform.position;
    }


    public void OnTriggerStay2D(Collider2D other)
    {
        ElementInstance element = other.GetComponent<ElementInstance>();
        if (element != null)
        {
            if (element != a && element != b)
            {
                Interact(element);
            }
        }
    }

    public abstract void Interact(ElementInstance element);
}
