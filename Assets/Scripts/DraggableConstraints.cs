using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DraggableConstraints : MonoBehaviour
{
    public Vector2 size;

    // Update is called once per frame
    void Update()
    {

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
