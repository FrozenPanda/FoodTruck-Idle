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
    private Vector3 screenPos;
    public void setParametersAndMove(Transform _target , IUnlockable _unlockable , Transform _canvasTransform)
    {
        transform.localScale *= 0.8f;
        
        _camera = Camera.main;
        moveAble = true;
        this._unlockable = _unlockable;
        this.target = _target;
        canvasTransform = _canvasTransform;
        
        go = Instantiate(nullImage, canvasTransform);
        screenPos = _camera.WorldToScreenPoint(target.position);
        go.GetComponent<Image>().rectTransform.position = screenPos;
        float rnd = 50f;
        transform.position += new Vector3(Random.Range(-rnd, rnd), Random.Range(-rnd, rnd), Random.Range(-rnd, rnd));
    }

    private void Update()
    {
        
        /*Vector3 screenPos = _camera.WorldToScreenPoint(target.position);
        go.GetComponent<Image>().rectTransform.position = screenPos;*/
        
        transform.position = Vector3.MoveTowards(transform.position , screenPos , movespeed * Time.deltaTime);

        if (Vector3.Distance(transform.position , screenPos) < 0.1f)
        {
            this._unlockable.moneyReached();
            Destroy(gameObject);
        }
    }
}
