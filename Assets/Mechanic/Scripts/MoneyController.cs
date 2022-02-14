using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MoneyController : MonoBehaviour
{
    public static MoneyController instance;

    public GameObject moneyToDrop;

    public RectTransform moneyStartPlace;
    public Transform moneyMoveCanvas;
    
    private void Awake()
    {
        instance = this;
    }

    public void CreateMoneyToUnlock(Transform targetPoint , IUnlockable _unlockable)
    {
        GameObject go = Instantiate(moneyToDrop,
            MoneyStartPos(),
            Quaternion.identity,
            moneyMoveCanvas);
        go.GetComponent<MoneyMove>().setParametersAndMove(targetPoint , _unlockable , moneyMoveCanvas);
    }

    private Vector3 MoneyStartPos()
    {
        return moneyStartPlace.position;
    }
}
