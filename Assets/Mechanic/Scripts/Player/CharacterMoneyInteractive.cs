using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoneyInteractive : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        IUnlockable _unlockable = other.GetComponent<IUnlockable>();
        if (_unlockable != null)
        {
            _unlockable.startMoneyDrop();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IUnlockable _unlockable = other.GetComponent<IUnlockable>();
        if (_unlockable != null)
        {
            _unlockable.stopMoneyDrop();
        }
    }
}
