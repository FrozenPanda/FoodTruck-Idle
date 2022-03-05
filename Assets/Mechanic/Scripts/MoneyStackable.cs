using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyStackable : MonoBehaviour
{
    private Vector3 defPos;
    private Quaternion defRot;
    private Transform targetTransform;
    private float timer = 0f;

    public AnimationCurve _gradient;
    public float _gradientPower;
    public Transform realMoney;
    
    public enum MoneyStackEvents
    {
        Idle,
        Move
    }

    private MoneyStackEvents _moneyStackEvents;
    
    public void StartMove(ICreatableAI _creatableAI)
    {
        MoneyCollectPlaces _moneyCollectPlaces = _creatableAI.GiveMeMoneyCollectPlaceData();
        targetTransform = _moneyCollectPlaces.takeLastElement();
        _moneyCollectPlaces.addMeToList(this);
        defPos = transform.position;
        defRot = transform.rotation;
        _moneyStackEvents = MoneyStackEvents.Move;
    }

    public void StartMove(MoneyCollectPlaces _moneyCollectPlaces)
    {
        targetTransform = _moneyCollectPlaces.takeLastElement();
        _moneyCollectPlaces.addMeToList(this);
        defPos = transform.position;
        defRot = transform.rotation;
        _moneyStackEvents = MoneyStackEvents.Move;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_moneyStackEvents)
        {
            case MoneyStackEvents.Idle:
                break;
            case MoneyStackEvents.Move:

                timer += Time.deltaTime;
                
                transform.position = Vector3.Lerp(defPos , targetTransform.position , timer);
                transform.rotation = Quaternion.Lerp(defRot , targetTransform.rotation , timer);
                realMoney.transform.localPosition = new Vector3(0f, _gradient.Evaluate(timer) * _gradientPower, 0f);
                
                if (timer >= 1)
                {
                    _moneyStackEvents = MoneyStackEvents.Idle;
                }
                
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
