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
    private Camera _camera;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _camera = Camera.main;
    }

    public void CreateMoneyToUnlock(Transform targetPoint , IUnlockable _unlockable  )
    {
        GameObject go = Instantiate(moneyToDrop,
            MoneyStartPos(),
            Quaternion.identity,
            moneyMoveCanvas);
        go.GetComponent<MoneyMove>().setParametersAndMove(targetPoint , _unlockable , moneyMoveCanvas, _camera);
    }

    private Vector3 MoneyStartPos()
    {
        return moneyStartPlace.position;
    }
}
