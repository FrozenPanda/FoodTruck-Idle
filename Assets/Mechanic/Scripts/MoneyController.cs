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

    public List<MoneyMove> allMoneyMove = new List<MoneyMove>();
    private int currentMoneyMove = 0;
    public void CreateMoneyToUnlock(Transform targetPoint)
    {
        allMoneyMove[currentMoneyMove].setParametersAndMove(targetPoint );
        
        if (currentMoneyMove < allMoneyMove.Count - 1)
        {
            currentMoneyMove++;
        }
        else
        {
            currentMoneyMove = 0;
        }
        
    }

    private Vector3 MoneyStartPos()
    {
        return moneyStartPlace.position;
    }
}
