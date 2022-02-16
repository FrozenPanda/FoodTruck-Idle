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
    
    private void Awake()
    {
        instance = this;
    }

    public void AddMetoTruckList(AItruckEat _aItruckEat)
    {
        aItruckEatsList.Add(_aItruckEat);
        _aItruckEat.CurrentQueuIndex = aItruckEatsList.Count - 1;
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
    
    void Start()
    {
        MaxQueu = SceneData.instance.hotDogQueuMaxCount;
        
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
        AIcreator.instance.CreateTruckQueuAI(this.enterPath , this.exitPath);
    }

    public void CustomerEatAlready()
    {
        throw new NotImplementedException();
    }

    public void CustomerOrderRequested()
    {
        throw new NotImplementedException();
    }
}
