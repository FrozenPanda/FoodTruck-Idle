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
        Banana
    }
    
    public enum moveEvent
    {
        Idle,
        ToCookPlace,
        ToPlayer,
        ToTable,
        End
    }

    public moveEvent _moveEvent;
    
    public void StartMove(Transform _parent , moveEvent _event)
    {
        transform.parent = _parent;
        targetPos = _parent.position;
        defaultPos = transform.localPosition;
        MoveTimer = 0f;
        this._moveEvent = _event;
        MoveAble = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (MoveAble)
        {
            MoveTimer += Time.deltaTime * 3f;
            transform.localPosition = Vector3.Lerp(defaultPos , Vector3.zero , MoveTimer );

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
