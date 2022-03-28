using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour
{
    public static SceneData instance;

    public PlayerAIMove[] allPlayerAI;
    public List<HotDogQueuManager> allTruck = new List<HotDogQueuManager>();

    public void AddMetoTruckList(HotDogQueuManager _truck) => allTruck.Add(_truck); 
    
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
    
    //VIPcustomer
    public int[] VIPEatMoneyDropPerLevel;
    public int sittingEatMoneyDropAmountVIP;
    public int standEatMoneyDropAmountVIP;
    
    //MoneyDrop
    public int sittingEatMoneyDropAmount;
    public int standEatMoneyDropAmount;
    
    //PlayerAI
    public int[] stuffMaxStack;
    public float[] stuffMoveSpeed;

    void Start()
    {
        CheckUpgrades();
    }

    public void CheckUpgrades()
    {
        SaveLoadSystem.Load();
        tableEatTime = tableEatTimePerUpgrade[SaveLoadSystem.instance.upgrades2[8 + UpgradeController2.instance.SceneIndex * 10]];
        customerMoveSpeed = customerMoveSpeedPerUpgrade[SaveLoadSystem.instance.upgrades2[9+ UpgradeController2.instance.SceneIndex * 10]];
        sittingEatMoneyDropAmountVIP = VIPEatMoneyDropPerLevel[SaveLoadSystem.instance.upgrades2[3 + UpgradeController2.instance.SceneIndex * 10]];
        standEatMoneyDropAmountVIP = VIPEatMoneyDropPerLevel[SaveLoadSystem.instance.upgrades2[3 + UpgradeController2.instance.SceneIndex * 10]];
    }
    // Update is called once per frame
    private float GameSpeed;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 10f;
        }
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            Time.timeScale = 1f;
        }
    }
}
