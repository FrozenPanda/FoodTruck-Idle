using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AImoneyDrop : MonoBehaviour
{
    public GameObject moneyToDrop;
    private int dropAmount = 3;

    public void StartMoneyDrop()
    {
        for (int i = 0; i < dropAmount; i++)
        {
            GameObject go = Instantiate(moneyToDrop, transform.position + Vector3.one, Quaternion.identity);
        }
    }
}
