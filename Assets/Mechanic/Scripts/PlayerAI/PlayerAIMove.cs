using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAIMove : MonoBehaviour
{
    public static PlayerAIMove instance;

    //0 middle, 1 table , 2 table , 3 table, 3 hotdog
    public Transform[] pathes;
    private AnimationController _animationController;
    private void Awake()
    {
        instance = this;
    }

    public Dictionary<AItableEat, int> waitingCustomers = new Dictionary<AItableEat, int>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            foreach (var _var in waitingCustomers)
            {
                Debug.Log("var" + _var.Key.MySeatID() + " value:" + _var.Value);
            }
        }

       EventController();
    }

    private void Start()
    {
        _animationController = GetComponent<AnimationController>();
        SendCharacterToTakeOrder();
    }

    public void AddMeToDictionary(AItableEat _aItableEat , int TableID)
    {
        waitingCustomers.Add(_aItableEat , TableID);
    }

    public void DeleteMeFromDictionary(AItableEat _aItableEat)
    {
        waitingCustomers.Remove(_aItableEat);
        
        return;
        
        foreach (var AIinDic in waitingCustomers)
        {
            if (AIinDic.Key ==_aItableEat)
            {
                waitingCustomers.Remove(AIinDic.Key);
            }
        }
    }

    private void EventController()
    {
        switch (_playerAIevents)
        {
            case PlayerAIevents.Idle:
                break;
            case PlayerAIevents.GoingForTakingOrder:
                break;
            case PlayerAIevents.WaitingCustomer:
                
                Debug.Log("Waiting Customer");
                if (waitingCustomers.Count > 0)
                {
                    PickOneTable();
                }
                
                break;
            case PlayerAIevents.GoingForCustomer:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    public enum PlayerAIevents
    {
        Idle,
        GoingForTakingOrder,
        WaitingCustomer,
        GoingForCustomer
    }

    public PlayerAIevents _playerAIevents;
    private int selectedTable;
    
    private void PickOneTable()
    {
        foreach (var _var in waitingCustomers)
        {
            selectedTable = _var.Value;
            SendAItoGiveOrder();
            break;
        }
    }

    private bool orderIsAvailable;
    public void CheckIfOrderStillNeed()
    {
        if (GetComponent<PlayerAISendTable>()._sendTableEvent != PlayerAISendTable.SendTableEvent.MovingToTable)
        {
            return;
        }
        
        orderIsAvailable = false;
        
        foreach (var _var in waitingCustomers)
        {
            if (_var.Value == selectedTable)
            {
                orderIsAvailable = true;
                break;
            }
        }

        if (!orderIsAvailable)
        {
            SelectNewTable();
        }
    }

    private void SelectNewTable()
    {
        if (waitingCustomers.Count > 0)
        {
            foreach (var _var in waitingCustomers)
            {
                selectedTable = _var.Value;
                SendAItoGiveOrder();
                break;
            }
        }
        else
        {
            _playerAIevents = PlayerAIevents.WaitingCustomer;
        }
    }
    
    public void CharacterReadyWithOrder()
    {
        _playerAIevents = PlayerAIevents.WaitingCustomer;
        _animationController.playAnim(0);
    }

    public void SendCharacterToTakeOrder()
    {
        GetComponent<PlayerAITakeOrder>().SendAItoTakeOrder();
        _playerAIevents = PlayerAIevents.Idle;
    }

    public void SendAItoGiveOrder()
    {
        GetComponent<PlayerAISendTable>().GiveOrder(selectedTable);
        _playerAIevents = PlayerAIevents.Idle;
    }
}
