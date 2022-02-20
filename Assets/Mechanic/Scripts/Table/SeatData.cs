using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatData : MonoBehaviour , ICreatableAI
{
    public int SeatID;
    public Transform enterPath;
    public Transform exitPath;
    public Transform sandwichPlace;
    public TableManager _tableManager;
    public AIcontroller currentAI;
    
    public void SendData()
    {
        AIcreator.instance.CreateTableAI(enterPath , exitPath , sandwichPlace , this , this);
    }

    public void CustomerEatAlready()
    {
        _tableManager.CustomerEatCallNextOne(SeatID);
    }

    public void CustomerOrderRequested()
    {
        _tableManager.OrderRequested(SeatID);
    }

    public MoneyCollectPlaces GiveMeMoneyCollectPlaceData()
    {
        return transform.parent.GetComponent<TableManager>()._moneyCollectPlaces;
    }
}
