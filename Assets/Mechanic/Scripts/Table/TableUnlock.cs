using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.NiceVibrations;

public class TableUnlock : MonoBehaviour , IUnlockable
{
    public int tableID;
    public int moneyToUnlock;
    private int moneyRequest;
    private int currentMoneyTaken;
    private float moneyRequestTimer = 0.05f;
    private float moneyrequestTimerSave = 0.05f;
    public GameObject realTable;

    public GameObject MoneyNeedUI;
    public Text MoneyNeedText;

    private int moneyValue = 10;
    
    public enum MoneyState
    {
        Idle,
        MoneyRequesting
    }

    public MoneyState _moneyState;
    
    void Start()
    {
        SaveLoadSystem.Load();

        MoneyNeedText.text = "$" + moneyToUnlock;

        if (SaveLoadSystem.instance.TableUnlock[tableID] == 1)
        {
            realTable.SetActive(true);
            GetComponent<BoxCollider>().enabled = false;
            MoneyNeedUI.SetActive(false);
        }

        if (moneyToUnlock > 900)
        {
            moneyValue = 20;
        }else if (moneyToUnlock > 400)
        {
            moneyValue = 10;
        }
        else
        {
            moneyValue = 5;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (_moneyState)
        {
            case MoneyState.Idle:
                break;
            case MoneyState.MoneyRequesting:

                if (PlayerMoneyData.instance.TotalMoney < moneyValue)
                {
                    return;
                }
                
                if (moneyRequest >= moneyToUnlock)
                {
                    return;
                }                
                
                if (moneyRequestTimer > 0f)
                {
                    moneyRequestTimer -= Time.deltaTime;
                }
                else
                {
                    MMVibrationManager.Haptic (HapticTypes.Success);
                    moneyRequest += moneyValue;
                    moneyrequestTimerSave *= 0.6f;
                    moneyRequestTimer = moneyrequestTimerSave;
                    MoneyController.instance.CreateMoneyToUnlock(transform , this);
                    PlayerMoneyData.instance.TotalMoney -= moneyValue;
                    if (moneyRequest >= moneyToUnlock)
                    {
                        _moneyState = MoneyState.Idle;
                    }
                }
                
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void startMoneyDrop()
    {
        _moneyState = MoneyState.MoneyRequesting;
    }

    public void stopMoneyDrop()
    {
        _moneyState = MoneyState.Idle;
        moneyrequestTimerSave = 0.05f;
    }

    public void moneyReached()
    {
        currentMoneyTaken += moneyValue;
        MoneyNeedText.text = "$" + (moneyToUnlock-currentMoneyTaken);

        if (currentMoneyTaken >= moneyToUnlock)
        {
            Debug.Log("TableEnabled");
            realTable.SetActive(true);
            SaveLoadSystem.instance.TableUnlock[tableID] = 1;
            SaveLoadSystem.Save();
            MoneyNeedUI.SetActive(false);
        }
    }
}
