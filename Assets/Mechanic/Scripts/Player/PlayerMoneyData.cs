using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoneyData : MonoBehaviour
{
    public static PlayerMoneyData instance;

    public Text totalMoney;
    
    private void Awake()
    {
        instance = this;
    }

    private int _totalMoney;

    public int TotalMoney
    {
        get => _totalMoney;
        set
        {
            _totalMoney = value;
            totalMoney.text = _totalMoney.ToString();
            SaveLoadSystem.instance.TotalMoney = _totalMoney;
            SaveLoadSystem.Save();
        }
    }

    void Start()
    {
        SaveLoadSystem.Load();
        _totalMoney = SaveLoadSystem.instance.TotalMoney;
        totalMoney.text = _totalMoney.ToString();
    }

    private void OnValidate()
    {
        totalMoney.text = _totalMoney.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            TotalMoney += 1000;
        }
    }

    public void IncreaseMoney()
    {
        TotalMoney += 1000;
    }
}
