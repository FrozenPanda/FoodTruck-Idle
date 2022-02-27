using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAISendTable : MonoBehaviour
{

    //0 mid , 1 table, 2 table, 3 table
    public Transform[] MovePaths;
    public Transform[] GoingToMidde;
    public Transform HoldingWithOrderPlace;
    private int CurrentTableID;
    public float moveSpeed;

    private AnimationController _animationController;
    private NavMeshAgent _navMeshAgent;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animationController = GetComponent<AnimationController>();
    }

    public void GiveOrder(int tableID)
    {
        Debug.Log("TAbleID:" + tableID + " requestedORder");
        //transform.LookAt(MovePaths[0]);
        CurrentTableID = tableID + 1;
        _sendTableEvent = SendTableEvent.MovingToTable;
    }

    public enum SendTableEvent
    {
        Idle,
        MovingToMiddle,
        MovingToTable,
        MovingMiddleWithHoldingOrder
    }

    public SendTableEvent _sendTableEvent;

    // Update is called once per frame
    void Update()
    {
        switch (_sendTableEvent)
        {
            case SendTableEvent.Idle:
                break;
            case SendTableEvent.MovingToMiddle:

                _navMeshAgent.SetDestination(MovePaths[0].position);
                //transform.position = Vector3.MoveTowards(transform.position , MovePaths[0].position , moveSpeed * Time.deltaTime);
                _animationController.playAnim(1 , moveSpeed);
                if (Vector3.Distance(transform.position , MovePaths[0].position) < 0.1f)
                {
                    //transform.LookAt(MovePaths[CurrentTableID]);
                    _sendTableEvent = SendTableEvent.MovingToTable;
                }
                
                break;
            case SendTableEvent.MovingToTable:
                _animationController.playAnim(1 , moveSpeed);

                _navMeshAgent.SetDestination(MovePaths[CurrentTableID].position);

                if (Vector3.Distance(transform.position , MovePaths[CurrentTableID].position) < 0.1f)
                {
                    if (!GetComponent<CharacterStackManager>().isCharacterCarrying())
                    {
                        GetComponent<PlayerAIMove>().SendCharacterToTakeOrder();
                        _sendTableEvent = SendTableEvent.Idle;
                    }
                    else
                    {
                        //transform.LookAt(MovePaths[0]);
                        _sendTableEvent = SendTableEvent.MovingMiddleWithHoldingOrder;
                    }
                }
                break;
            
            case SendTableEvent.MovingMiddleWithHoldingOrder:
                _navMeshAgent.SetDestination(HoldingWithOrderPlace.position);
                _animationController.playAnim(1, moveSpeed);
                if (Vector3.Distance(transform.position , HoldingWithOrderPlace.position) < 0.1f)
                {
                    GetComponent<PlayerAIMove>()._playerAIevents = PlayerAIMove.PlayerAIevents.WaitingCustomer;
                    _sendTableEvent = SendTableEvent.Idle;
                }
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
