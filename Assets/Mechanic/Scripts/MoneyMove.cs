using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyMove : MonoBehaviour
{
    private bool moveAble;
    private IUnlockable _unlockable;
    private Transform target;
    private Camera _camera;
    private Transform canvasTransform;
    public GameObject nullImage;
    private GameObject go;
    public void setParametersAndMove(Transform _target , IUnlockable _unlockable , Transform _canvasTransform)
    {
        _camera = Camera.main;
        moveAble = true;
        this._unlockable = _unlockable;
        this.target = _target;
        canvasTransform = _canvasTransform;
        
        go = Instantiate(nullImage, canvasTransform);
        Vector3 screenPos = _camera.WorldToScreenPoint(target.position);
        go.GetComponent<Image>().rectTransform.position = screenPos;
    }

    private void Update()
    {
        
        Vector3 screenPos = _camera.WorldToScreenPoint(target.position);
        go.GetComponent<Image>().rectTransform.position = screenPos;
        
        transform.position = Vector3.MoveTowards(transform.position , screenPos , 1000f * Time.deltaTime);

        if (Vector3.Distance(transform.position , target.position) < 0.1f)
        {
            this._unlockable.moneyReached();
            Destroy(gameObject);
        }
    }
}
