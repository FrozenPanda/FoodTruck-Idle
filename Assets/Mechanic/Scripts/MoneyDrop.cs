using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MoneyDrop : MonoBehaviour
{
    private Rigidbody rb;
    private float throwSpeed = 100f;
    void Start()
    {
        transform.tag = "MoneyDrop";
        gameObject.AddComponent<BoxCollider>().isTrigger = true;
        
        rb = GetComponent<Rigidbody>();
        
        
        Vector3 randomVal = new Vector3(Random.Range(-throwSpeed , throwSpeed), 200f , Random.Range(-throwSpeed , throwSpeed));
        transform.rotation = Quaternion.Euler(0f, Random.Range(0f,360f) ,0f);
        rb.AddForce(randomVal);
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
