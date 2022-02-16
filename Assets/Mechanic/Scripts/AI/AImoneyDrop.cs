using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AImoneyDrop : MonoBehaviour
{
    public GameObject moneyToDrop;
    public int dropAmount = 3;

    public void StartMoneyDrop()
    {
        if (dropAmount < 1)
        {
            dropAmount = 3;
        }
        
        for (int i = 0; i < dropAmount; i++)
        {
            GameObject go = Instantiate(moneyToDrop, transform.position + Vector3.up, Quaternion.identity);
        }
    }
}
