using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideInMenu : MonoBehaviour
{
    private Animator _animator;
    protected bool _open;
    public void Awake()
    {
        _animator = GetComponent<Animator>();
    }


    public virtual void ToggleMenu()
    {
        _open = !_open;
        _animator.SetBool("Open", _open);
    }
}
