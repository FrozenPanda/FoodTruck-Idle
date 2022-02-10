using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance;

    private void Awake()
    {
        instance = this;
    }

    // AI behaviour
    public float BotMoveSpeed;
    public float BotEatSpeed;
    
    
}
