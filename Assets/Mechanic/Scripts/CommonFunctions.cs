using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommonFunctions : MonoBehaviour
{
    public static CommonFunctions instance;

    private void Awake()
    {
        instance = this;
    }

    public void CreateAnyUI(GameObject GO, Transform target, Transform canvas)
    {
        GameObject go = Instantiate(GO, canvas);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position);
        go.GetComponent<Image>().rectTransform.position = screenPos;
    }
}
