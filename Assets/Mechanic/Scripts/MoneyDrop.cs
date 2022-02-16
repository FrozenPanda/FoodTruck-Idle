using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MoneyDrop : MonoBehaviour
{
    private Rigidbody rb;
    public float throwSpeedForward = 100f;
    public float throwSpeedUp;
    public float YdegreeMin;
    public float YdegreeMax;
    void Start()
    {
        
        transform.tag = "MoneyDrop";
        gameObject.AddComponent<BoxCollider>().isTrigger = true;
        
        rb = GetComponent<Rigidbody>();
        
        transform.rotation = Quaternion.Euler(0f, Random.Range(YdegreeMin,YdegreeMax) ,0f);
        rb.AddForce(transform.forward * Random.Range(throwSpeedForward/2 , throwSpeedForward*1.5f) + transform.up * Random.Range(throwSpeedUp/2f , throwSpeedUp*1.5f));
        groundMask = LayerMask.GetMask("Ground");
    }

    private RaycastHit hit;
    private LayerMask groundMask;
    void Update()
    {
        if (Physics.Raycast(transform.position + Vector3.up , Vector3.down , out hit , 1.2f,groundMask))
        {
            if (hit.transform)
            {
                transform.position = hit.point + Vector3.up * 0.2f;
                rb.velocity = Vector3.zero;
                rb.useGravity = false;
            }
        }
        
        transform.Rotate(0f , 90f * Time.deltaTime, 0f) ;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CreateCoinImage(transform);
            Destroy(gameObject);
        }
    }
    
    
    private void CreateCoinImage(Transform _transform)
    {
        GameObject go = Instantiate(SceneReferences.instance.moneyMoveImage, SceneReferences.instance.moneyMoveCanvas);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(_transform.position);
        go.GetComponent<Image>().rectTransform.position = screenPos;
    }
}
