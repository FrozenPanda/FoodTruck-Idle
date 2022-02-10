using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyMove : MonoBehaviour
{
    private bool moveAble;
    private IUnlockable _unlockable;
    private Transform target;

    public void setParametersAndMove(Transform _target , IUnlockable _unlockable)
    {
        moveAble = true;
        this._unlockable = _unlockable;
        this.target = _target;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position , target.position , 10f * Time.deltaTime);

        if (Vector3.Distance(transform.position , target.position) < 0.1f)
        {
            this._unlockable.moneyReached();
            Destroy(gameObject);
        }
    }
}
