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
    public GameObject moneyMoveImage2;
    public Vector3 moneyMoveImage2Target;
    public Text TotalMoneyText;
    public int CurrentMoneyAmount;

    public GameObject HandFullImage;
    
    public GameObject eatingParticle;
    
    void Start()
    {
        CurrentMoneyAmount = 500;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
