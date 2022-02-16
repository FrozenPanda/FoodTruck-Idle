using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AImoneyDrop : MonoBehaviour
{
    public GameObject moneyToDrop;
    public int dropAmount = 3;

    public void StartMoneyDrop(int _amount)
    {
        for (int i = 0; i < _amount; i++)
        {
            GameObject go = Instantiate(moneyToDrop, transform.position + Vector3.up, Quaternion.identity);
        }
    }
}
