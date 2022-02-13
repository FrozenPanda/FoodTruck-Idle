using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneReferences : MonoBehaviour
{
    public static SceneReferences instance;

    private void Awake()
    {
        instance = this;
    }

    public Transform moneyMoveCanvas;
    public Transform moneyTargetMoveImage;
    public GameObject moneyMoveImage;
    public Text TotalMoneyText;
    public int CurrentMoneyAmount;
    
    void Start()
    {
        CurrentMoneyAmount = 500;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}