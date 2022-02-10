using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathHighlightChild : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        PathHighlight _highlight = transform.parent.GetComponent<PathHighlight>();
        if (_highlight)
        {
            _highlight.OnDrawGizmosSelected();
        }
    }
}
