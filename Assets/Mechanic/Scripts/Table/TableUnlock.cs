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
    private float vibratorTime = 0.5f;

    private MoneyController _moneyController;
    private PlayerMoneyData _playerMoneyData;

    private float moneyTextUpdateTimer;
    
    public enum MoneyState
    {
        Idle,
        MoneyRequesting
    }

    public MoneyState _moneyState;
    
    
    
    void Start()
    {
        SaveLoadSystem.Load();
        
        _moneyController = MoneyController.instance;
        _playerMoneyData = PlayerMoneyData.instance;
        

        if (SaveLoadSystem.instance.TableUnlock[tableID] == 1)
        {
            realTable.SetActive(true);
            GetComponent<BoxCollider>().enabled = false;
            MoneyNeedUI.SetActive(false);
        }
        else
        {
            if (SaveLoadSystem.instance.TableUnlockRemainMoney[tableID] > 0)
            {
                moneyToUnlock -= SaveLoadSystem.instance.TableUnlockRemainMoney[tableID];
            }
        }
        
        MoneyNeedText.text = "$" + moneyToUnlock;

        if (moneyToUnlock > 900)
        {
            moneyValue = 100;
        }else if (moneyToUnlock > 400)
        {
            moneyValue = 25;
        }
        else
        {
            moneyValue = 10;
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

                if (_playerMoneyData.TotalMoney < moneyValue)
                {
                    return;
                }
                
                if (moneyRequest >= moneyToUnlock)
                {
                    return;
                }     
                
                if (vibratorTime > 0f)
                {
                    vibratorTime -= Time.deltaTime;
                }
                else
                {
                    MMVibrationManager.Haptic (HapticTypes.MediumImpact);
                    vibratorTime = 0.5f;
                }
                
                if (moneyRequestTimer > 0f)
                {
                    moneyRequestTimer -= Time.deltaTime;
                }
                else
                {
                    
                    moneyRequest += moneyValue;
                    
                   // moneyrequestTimerSave = 0.1f;
                   moneyRequestTimer = 0.1f;
                    moneyReached();
                    _moneyController.CreateMoneyToUnlock(transform);
                    _moneyController.CreateMoneyToUnlock(transform);
                    
                    //CreateCoinImage();
                    _playerMoneyData.TotalMoney -= moneyValue;
                    
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
        SceneReferences.instance.moneyMoveImage2Target = Camera.main.WorldToScreenPoint(transform.position);
        
    }

    public void stopMoneyDrop()
    {
        _moneyState = MoneyState.Idle;
        moneyrequestTimerSave = 0.1f;
        int x = moneyToUnlock - currentMoneyTaken;
        MoneyNeedText.text = "$" + x;
        moneyTextUpdateTimer = 0.5f;
        SaveLoadSystem.Load();
        SaveLoadSystem.instance.TableUnlockRemainMoney[tableID] = currentMoneyTaken;
        SaveLoadSystem.Save();
    }
    
    private void CreateCoinImage()
    {
        GameObject go = Instantiate(SceneReferences.instance.moneyMoveImage2, SceneReferences.instance.moneyMoveCanvas);
        Debug.Log("Create Coin Image");
        //Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        //go.GetComponent<Image>().rectTransform.position = screenPos;
    }

    public void moneyReached()
    {
        currentMoneyTaken += moneyValue;
        int x = moneyToUnlock - currentMoneyTaken;
        MoneyNeedText.text = "$" + x;

        if (currentMoneyTaken >= moneyToUnlock)
        {
            //Debug.Log("TableEnabled");
            realTable.SetActive(true);
            SaveLoadSystem.instance.TableUnlock[tableID] = 1;
            SaveLoadSystem.Save();
            MoneyNeedUI.SetActive(false);
            
        }
    }
}
