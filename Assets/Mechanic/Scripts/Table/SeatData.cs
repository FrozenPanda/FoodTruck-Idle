using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatData : MonoBehaviour , ICreatableAI
{
    public MoveAbleMeal.MealType _mealType;
    private int _mealTypeIndex;
    public int SeatID;
    public Transform enterPath;
    public Transform exitPath;
    public Transform sandwichPlace;
    public TableManager _tableManager;
    public AIcontroller currentAI;
    
    public void SendData()
    {
        _mealType = transform.parent.GetComponent<TableManager>()._mealType;
        switch (_mealType)
        {
            case MoveAbleMeal.MealType.Hotdog:
                _mealTypeIndex = 0;
                break;
            case MoveAbleMeal.MealType.Pizza:
                _mealTypeIndex = 2;
                break;
            case MoveAbleMeal.MealType.Hamburger:
                _mealTypeIndex = 1;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        AIcreator.instance.CreateTableAI(enterPath , exitPath , sandwichPlace , this , this , _mealTypeIndex);
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

    public int GetSeatID()
    {
        return transform.parent.GetComponent<TableManager>().TableID;
    }

    public int GetBotIndex()
    {
        return transform.parent.GetComponent<TableManager>().botIndex;
    }


    public HotDogQueuManager sendMeHotDogQueuManagerData()
    {
        return null;
    }
}
