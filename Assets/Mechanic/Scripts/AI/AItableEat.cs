using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AItableEat : MonoBehaviour
{
    public enum TableEvent
    {
        Idle,
        Placing,
        OrderRequested,
        Eating,
        StandingUp,
        FollowExit,
        End
    }

    public TableEvent _tableEvent;

    private float turnSpeed = 0f;
    private Quaternion defaultRot;
    private Quaternion lookAt;

    private ICreatableAI _creatableAI;

    private float eatTime = 2f; //todo bunu save load systemden çek
    
    public void SetParameters(Transform lookPlace , ICreatableAI _creatableAI)
    {
        this._creatableAI = _creatableAI;
        defaultRot = transform.rotation;
        lookAt = Quaternion.LookRotation(new Vector3(lookPlace.position.x , transform.position.y , lookPlace.position.z) - transform.position);
        _tableEvent = TableEvent.Placing;
    }

    public void StartEating()
    {
        _tableEvent = TableEvent.Eating;
    }
    
    private void Update()
    {
        switch (_tableEvent)
        {
            case TableEvent.Idle:
                break;
            case TableEvent.Placing:

                turnSpeed += Time.deltaTime * 2f;
                transform.rotation = Quaternion.Lerp(defaultRot , lookAt , turnSpeed);

                if (turnSpeed > 1f)
                {
                    _tableEvent = TableEvent.OrderRequested;
                }
                
                break;
            case TableEvent.OrderRequested:

                _creatableAI.CustomerOrderRequested();
                _tableEvent = TableEvent.Idle;
                
                break;
            case TableEvent.Eating:

                if (eatTime > 0f)
                {
                    eatTime -= Time.deltaTime;
                }
                else
                {
                    _tableEvent = TableEvent.FollowExit;
                }
                
                break;
            case TableEvent.StandingUp:
                break;
            case TableEvent.FollowExit:
                
                GetComponent<AIcontroller>().SendAItoFinish();
                _tableEvent = TableEvent.End;
                
                break;
            case TableEvent.End:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
