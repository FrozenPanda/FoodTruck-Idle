using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableUnlock : MonoBehaviour , IUnlockable
{
    public int tableID;
    public int moneyToUnlock;
    private int moneyRequest;
    private int currentMoneyTaken;
    private float moneyRequestTimer = 0.05f;
    public GameObject realTable;
    
    public enum MoneyState
    {
        Idle,
        MoneyRequesting
    }

    public MoneyState _moneyState;
    
    void Start()
    {
        SaveLoadSystem.Load();

        if (SaveLoadSystem.instance.TableUnlock[tableID] == 1)
        {
            realTable.SetActive(true);
            GetComponent<BoxCollider>().enabled = false;
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

                if (moneyRequestTimer > 0f)
                {
                    moneyRequestTimer -= Time.deltaTime;
                }
                else
                {
                    moneyRequest++;
                    MoneyController.instance.CreateMoneyToUnlock(transform , this);
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
    }

    public void moneyReached()
    {
        currentMoneyTaken++;

        if (currentMoneyTaken >= moneyToUnlock)
        {
            Debug.Log("TableEnabled");
            realTable.SetActive(true);
            SaveLoadSystem.instance.TableUnlock[tableID] = 1;
            SaveLoadSystem.Save();
        }
    }
}
