using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathHighlight : MonoBehaviour
{
    public Gradient _gradient;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Transform[] childs;
    public void OnDrawGizmosSelected()
    {
        childs = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            childs[i] = transform.GetChild(i);
            if (childs[i].GetComponent<PathHighlightChild>() == null)
            {
                childs[i].gameObject.AddComponent<PathHighlightChild>();
            }
        }

        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Vector3 toDraw = childs[i + 1].position - childs[i].position;
            Gizmos.color = _gradient.Evaluate((float) i / (float) childs.Length);
            Gizmos.DrawRay(childs[i].position , toDraw );
        }
    }
}
