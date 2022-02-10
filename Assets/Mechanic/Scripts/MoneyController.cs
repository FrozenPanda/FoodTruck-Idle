using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoneyController : MonoBehaviour
{
    public static MoneyController instance;

    public GameObject moneyToDrop;
    
    private void Awake()
    {
        instance = this;
    }

    public void CreateMoneyToUnlock(Transform targetPoint , IUnlockable _unlockable)
    {
        GameObject go = Instantiate(moneyToDrop,
            transform.position + Vector3.up * 5f + new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)),
            Quaternion.identity);
        go.GetComponent<MoneyMove>().setParametersAndMove(targetPoint , _unlockable);
    }
}
