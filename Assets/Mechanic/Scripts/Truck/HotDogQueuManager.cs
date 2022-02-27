using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotDogQueuManager : MonoBehaviour, ICreatableAI
{
    public static HotDogQueuManager instance;
    
    public Transform queuPlaceHolder;
    public Transform[] queuPlaces;
    public List<AItruckEat> aItruckEatsList = new List<AItruckEat>();

    public Transform enterPath;
    public Transform exitPath;

    private int MaxQueu = 5;
    public MoneyCollectPlaces _moneyCollectPlaces;

    public MoveAbleMeal.MealType _mealType;
    
    private void Awake()
    {
        instance = this;
    }

    public void AddMetoTruckList(AItruckEat _aItruckEat)
    {
        aItruckEatsList.Add(_aItruckEat);
        _aItruckEat.CurrentQueuIndex = aItruckEatsList.Count - 1;
    }

    public HotDogQueuManager sendMeHotDogQueuManagerData()
    {
        return this;
    }

    public void NextOne()
    {
        aItruckEatsList.RemoveAt(0);
        for (int i = 0; i < aItruckEatsList.Count; i++)
        {
            aItruckEatsList[i].MoveNext();
        }
        
        AIcreator.instance.AddMeTolist(this);
    }

    public int[] MaxQueuPerUpgrade;
    void Start()
    {
        SaveLoadSystem.Load();
        
        MaxQueu = MaxQueuPerUpgrade[SaveLoadSystem.instance.upgrades2[2]];
        
        queuPlaces = new Transform[queuPlaceHolder.childCount];

        for (int i = 0; i < queuPlaces.Length; i++)
        {
            queuPlaces[i] = queuPlaceHolder.GetChild(i);
        }
        
        for (int i = 0; i < MaxQueu; i++)
        {
            AIcreator.instance.AddMeTolist(this);
        }
    }

    public void SendData()
    {
        int mealIndex = -1;
        switch (_mealType)
        {
            case MoveAbleMeal.MealType.Hotdog:
                mealIndex = 0;
                break;
            case MoveAbleMeal.MealType.Pizza:
                mealIndex = 2;
                break;
            case MoveAbleMeal.MealType.Hamburger:
                mealIndex = 1;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        AIcreator.instance.CreateTruckQueuAI(this.enterPath , this.exitPath , this , mealIndex);
    }

    public void CustomerEatAlready()
    {
        throw new NotImplementedException();
    }

    public void CustomerOrderRequested()
    {
        throw new NotImplementedException();
    }

    public void UpgradeOccured()
    {
        AIcreator.instance.AddMeTolist(this);
    }

    public MoneyCollectPlaces GiveMeMoneyCollectPlaceData()
    {
        return _moneyCollectPlaces;
    }

    public int GetSeatID()
    {
        return 18;
    }
}
