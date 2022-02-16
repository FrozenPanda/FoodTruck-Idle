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
    public float tableEatTime;
    
    //Customers
    public float customerMoveSpeed;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
