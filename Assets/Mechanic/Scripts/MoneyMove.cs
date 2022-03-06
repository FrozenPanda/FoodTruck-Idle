using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MoneyMove : MonoBehaviour
{
    private bool moveAble;
    private IUnlockable _unlockable;
    private Transform target;
    private Camera _camera;
    private Transform canvasTransform;
    public GameObject nullImage;
    private GameObject go;
    public float movespeed;
    private float lerpMove;
    private Vector3 screenPos;
    public RectTransform imageRectTransform;
    public RectTransform moneyStartPlace;
    private void Start()
    {
        transform.localScale = Vector3.zero;
        _camera = Camera.main;
    }

    public void setParametersAndMove(Transform _target)
    {
        transform.localScale = Vector3.one * 0.8f;
        //transform.localScale *= 0.8f;
        transform.position = moneyStartPlace.position;
        
        moveAble = true;
        
       //canvasTransform = _canvasTransform;
        lerpMove = 0f;
        //go = Instantiate(nullImage, canvasTransform);
        screenPos = _camera.WorldToScreenPoint(_target.position);
        //go.GetComponent<Image>().rectTransform.position = screenPos;
         float rnd = 100f;
        transform.position += new Vector3(Random.Range(-rnd, rnd), Random.Range(-rnd, rnd), 0f);
    }

    private void Update()
    {
        if (!moveAble)
        {
            return;
        }
        /*Vector3 screenPos = _camera.WorldToScreenPoint(target.position);
        go.GetComponent<Image>().rectTransform.position = screenPos;*/
        lerpMove += Time.deltaTime*2f;
        transform.position = Vector3.Lerp(transform.position , screenPos , lerpMove);

        if (lerpMove > 1f)
        {
            //this._unlockable.moneyReached();
            moveAble = false;
            transform.localScale = Vector3.zero;
        }
    }
}
