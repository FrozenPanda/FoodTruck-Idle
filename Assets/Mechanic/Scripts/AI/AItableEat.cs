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

    private float eatTime = 5f; //todo bunu save load systemden çek
    private int MyTableID;
    
    public void SetParameters(Transform lookPlace , ICreatableAI _creatableAI)
    {
        eatTime = SceneData.instance.tableEatTime;
        this._creatableAI = _creatableAI;
        MyTableID = _creatableAI.GetSeatID();
        defaultRot = transform.rotation;
        lookAt = Quaternion.LookRotation(new Vector3(lookPlace.position.x , transform.position.y , lookPlace.position.z) - transform.position);
        _tableEvent = TableEvent.Placing;
        GetComponent<AIcontroller>().wantedCanvas.SetActive(false);
    }

    public void StartEating()
    {
        if (PlayerAIMove.instance)
        {
            PlayerAIMove.instance.DeleteMeFromDictionary(this);
        }
        GetComponent<AIcontroller>().wantedCanvas.SetActive(false);
        StartCoroutine(StartEatingDelay());
    }

    IEnumerator StartEatingDelay()
    {
        yield return new WaitForSeconds(0.5f);
        
        _tableEvent = TableEvent.Eating;
        GetComponent<AIanimController>().playAnimWithName("eat");
        GameObject go = Instantiate(SceneReferences.instance.eatingParticle, transform.position + transform.forward * 0.1f,
            SceneReferences.instance.eatingParticle.transform.rotation);
        go.transform.rotation = transform.rotation;
        Destroy(go , eatTime);
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

                if (PlayerAIMove.instance)
                {
                    PlayerAIMove.instance.AddMeToDictionary(this , MySeatID());
                }
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

    public int MySeatID()
    {
        return MyTableID;
    }
}
