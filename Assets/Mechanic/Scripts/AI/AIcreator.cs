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

    public int VIPcustomerRatio;
    private int currentVIPratio;

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
            tableAIcreateWaitTime = 1f / (AIcreateWaitingList.Count);
            AIcreateWaitingList[0].SendData();
            AIcreateWaitingList.RemoveAt(0);
        }
    }

    private void createAIforTruck()
    {
        
    }

    public void CreateTruckQueuAI(Transform enterPath , Transform exitPath , ICreatableAI _creatableAI , int customerWantsImageIndex = 0)
    {
        GameObject go = Instantiate(pickRandomAI(), AIstartPos[0].position, Quaternion.identity);
        AIcontroller _aIcontroller = go.GetComponent<AIcontroller>();
        _aIcontroller.SetAIAgent(enterPath , exitPath  , AIcontroller.AIevent.StandEat, _creatableAI , customerWantsImageIndex);
        
    }

    public void CreateTableAI(Transform enterPath, Transform exitPath , Transform sandwichPlace , ICreatableAI _creatableAI , SeatData _seatData , int _MealType)
    {
        GameObject go = Instantiate(pickRandomAI(), AIstartPos[0].position, Quaternion.identity);
        AIcontroller _aIcontroller = go.GetComponent<AIcontroller>();
        _aIcontroller.SetAIAgent(enterPath , exitPath , AIcontroller.AIevent.TableEat ,_creatableAI ,sandwichPlace ,_MealType );
        _seatData.currentAI = _aIcontroller;
    }

    private GameObject pickRandomAI()
    {
        if (VIPalways)
        {
            return allAI[1];  
        }
        
        if (currentVIPratio < VIPcustomerRatio)
        {
            currentVIPratio++;
            return allAI[0];
        }
        else
        {
            currentVIPratio = 0;
            return allAI[1];   
        }
    }

    private bool VIPalways;

    public bool ViPalways
    {
        get => VIPalways;
        set => VIPalways = value;
    }
}
