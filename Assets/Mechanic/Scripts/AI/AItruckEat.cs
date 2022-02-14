using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AItruckEat : MonoBehaviour
{
    
    public enum AItruckEatEvents
    {
        Idle,
        Walkable,
        Moving,
        Eating,
        FollowExit
    }

    public AItruckEatEvents _aItruckEatEvents;

    private int currentQueuIndex;
    private Transform target;
    public float moveSpeed;
    public float eatTime;
    private float eatTimeDefault;
    
    private void Start()
    {
        moveSpeed = 3f;
        eatTime = 2f;
        HotDogQueuManager.instance.AddMetoTruckList(this);
        eatTimeDefault = eatTime;
    }

    public int CurrentQueuIndex
    {
        set { currentQueuIndex = value; }
    }
    
    public void SetParamatersAndGo()
    {
        _aItruckEatEvents = AItruckEatEvents.Moving;
        target = HotDogQueuManager.instance.queuPlaces[currentQueuIndex];
    }

    private void Update()
    {
        switch (_aItruckEatEvents)
        {
            case AItruckEatEvents.Idle:
                break;
            case AItruckEatEvents.Moving:
                
                transform.position = Vector3.MoveTowards(transform.position , target.position , moveSpeed* Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation , target.rotation , moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position , target.position) < 0.1f)
                {
                    DestinationReached();            
                }
                
                break;
            case AItruckEatEvents.Eating:

                if (eatTime > 0f)
                {
                    eatTime -= Time.deltaTime;
                }
                else
                {
                    GetComponent<AIcontroller>().SendAItoFinish();
                    _aItruckEatEvents = AItruckEatEvents.FollowExit;
                    FinishEatingMoveOthers();
                }
                
                break;
            case AItruckEatEvents.FollowExit:
                break;
            case AItruckEatEvents.Walkable:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void FinishEatingMoveOthers()
    {
        HotDogQueuManager.instance.NextOne();
    }

    public void MoveNext()
    {
        currentQueuIndex--;
        target = HotDogQueuManager.instance.queuPlaces[currentQueuIndex];

        if (_aItruckEatEvents == AItruckEatEvents.Walkable || _aItruckEatEvents == AItruckEatEvents.Moving)
        {
            _aItruckEatEvents = AItruckEatEvents.Moving;
            GetComponent<AIanimController>().playAnimWithName("walk");
        }
    }

    private void DestinationReached()
    {
        if (currentQueuIndex == 0)
        {
            _aItruckEatEvents = AItruckEatEvents.Eating;
            GetComponent<AIanimController>().playAnimWithName("Idle");
        }
        else
        {
            _aItruckEatEvents = AItruckEatEvents.Walkable;
            GetComponent<AIanimController>().playAnimWithName("Idle");
        }
    }
}
