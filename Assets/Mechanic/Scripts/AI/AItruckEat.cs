using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AItruckEat : MonoBehaviour
{
    public static AItruckEat instance;
    public List<AItruckEat> truckQueu = new List<AItruckEat>();
    public Transform EnterPath;
    public Transform ExitPath;
    private int MaxQueuNumber = 3;
    
    private void Awake()
    {
        instance = this;
    }

    public void AddMeToList(AItruckEat _aItruckEat)
    {
        truckQueu.Add(_aItruckEat);
    }

    private void StartCustomersAtStartOftheGame(int maxPerson)
    {
        for (int i = 0; i < maxPerson; i++)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
