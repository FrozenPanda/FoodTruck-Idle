using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour
{
    public static SceneData instance;

    private void Awake()
    {
        instance = this;
    }

    //HotDogTruck
    public float hotDogQueuWaitTimer;
    public int hotDogQueuMaxCount;
    
    //Table
    public float[] tableEatTimePerUpgrade;
    public float tableEatTime;
    
    //Customers
    public float[] customerMoveSpeedPerUpgrade;
    public float customerMoveSpeed;
    
    //MoneyDrop
    public int sittingEatMoneyDropAmount;
    public int standEatMoneyDropAmount;

    void Start()
    {
        
    }

    public void CheckUpgrades()
    {
        SaveLoadSystem.Load();
        tableEatTime = tableEatTimePerUpgrade[SaveLoadSystem.instance.upgrades2[8]];
        customerMoveSpeed = customerMoveSpeedPerUpgrade[SaveLoadSystem.instance.upgrades2[9]];
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
