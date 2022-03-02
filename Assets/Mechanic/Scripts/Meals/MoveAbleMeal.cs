using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAbleMeal : MonoBehaviour
{
    private Vector3 defaultPos;
    private Vector3 targetPos;
    private float MoveTimer;

    private Transform beChild;
    private bool MoveAble;
    
    
    public enum MealType
    {
        Hotdog,
        Pizza,
        Hamburger,
        KFC
    }

    public MealType _mealType;
    
    public enum moveEvent
    {
        Idle,
        ToCookPlace,
        ToPlayer,
        ToTable,
        End
    }

    public moveEvent _moveEvent;

    public AnimationCurve _animationCurve;
    public Transform realHotDog;
    public float upDownPower;
    public float MoveSpeed;

    public void StartMove(Transform _parent , moveEvent _event , bool makeUpDownSpeedZero = false)
    {
        transform.parent = _parent;
        targetPos = _parent.position;
        defaultPos = transform.localPosition;
        MoveTimer = 0f;
        this._moveEvent = _event;

        if (makeUpDownSpeedZero)
        {
            upDownPower = 0f;
        }
        
        MoveAble = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (MoveAble)
        {
            MoveTimer += Time.deltaTime * MoveSpeed;
            transform.localPosition = Vector3.Lerp(defaultPos , Vector3.zero , MoveTimer );
            realHotDog.transform.localPosition = new Vector3(0f, _animationCurve.Evaluate(MoveTimer) * upDownPower, 0f);
            
            if (MoveTimer > 1f)
            {
                MoveAble = false;
                MoveEnd();
            }
        }
    }

    private void MoveEnd()
    {
        switch (_moveEvent)
        {
            case moveEvent.Idle:
                break;
            case moveEvent.ToCookPlace:
                break;
            case moveEvent.ToPlayer:
                break;
            case moveEvent.ToTable:
                break;
            case moveEvent.End:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
