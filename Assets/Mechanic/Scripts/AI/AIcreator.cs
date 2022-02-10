using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIcreator : MonoBehaviour
{
    public static AIcreator instance;

    private void Awake()
    {
        instance = this;
    }

    public List<ICreatableAI> AIcreateWaitingList = new List<ICreatableAI>();
    public GameObject[] allAI;
    public Transform[] AIstartPos;

    public void AddMeTolist(ICreatableAI _creatableAI)
    {
        AIcreateWaitingList.Add(_creatableAI);
    }

    private void Update()
    {
        createAIforTable();
    }

    private float tableAIcreateWaitTime;
    private void createAIforTable()
    {
        if (AIcreateWaitingList.Count < 1)
        {
            return;
        }

        if (tableAIcreateWaitTime > 0f)
        {
            tableAIcreateWaitTime -= Time.deltaTime;
        }
        else
        {
            tableAIcreateWaitTime = 1f;
            AIcreateWaitingList[0].SendData();
            AIcreateWaitingList.RemoveAt(0);
        }
    }

    public void CreateTableAI(Transform enterPath, Transform exitPath , Transform sandwichPlace , ICreatableAI _creatableAI , SeatData _seatData)
    {
        GameObject go = Instantiate(pickRandomAI(), AIstartPos[0].position, Quaternion.identity);
        AIcontroller _aIcontroller = go.GetComponent<AIcontroller>();
        _aIcontroller.SetAIAgent(enterPath , exitPath , AIcontroller.AIevent.TableEat ,_creatableAI ,sandwichPlace);
        _seatData.currentAI = _aIcontroller;
    }

    private GameObject pickRandomAI()
    {
        return allAI[Random.Range(0, allAI.Length)];
    }
}