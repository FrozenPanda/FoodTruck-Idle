using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    public GameObject UpgradeCanvas;
    public GameObject HotDogAI;

    public void EnableHotDogAI()
    {
        PlayerAIMove.instance.EnableAI();
        UpgradeCanvas.SetActive(false);
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UpgradeCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            UpgradeCanvas.SetActive(false);
        }
    }
}
