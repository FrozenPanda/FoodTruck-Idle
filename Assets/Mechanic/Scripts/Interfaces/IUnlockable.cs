using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnlockable
{
    public void startMoneyDrop();

    public void stopMoneyDrop();

    public void moneyReached();
}
