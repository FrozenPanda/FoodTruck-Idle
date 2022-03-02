using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAIdatabase : MonoBehaviour
{
    public List<PlayerAIMove> AllPlayerAIlist = new List<PlayerAIMove>();

    public static PlayerAIdatabase instance;

    private void Awake()
    {
        instance = this;
    }

    public void AddMeToDictionary(AItableEat _aItableEat , int _seatID , int _botIndex)
    {
        for (int i = 0; i < AllPlayerAIlist.Count; i++)
        {
            AllPlayerAIlist[i].AddMeToDictionary(_aItableEat , _seatID , _botIndex);
        }
    }

    public void DeleteMeFromDictionary(AItableEat _aItableEat , int _botIndex)
    {
        for (int i = 0; i < AllPlayerAIlist.Count; i++)
        {
            AllPlayerAIlist[i].DeleteMeFromDictionary(_aItableEat , _botIndex);
        }
    }
    
}
