using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgradeController : MonoBehaviour
{
    public static PlayerUpgradeController instance;

    private void Awake()
    {
        instance = this;
    }

    public void characterStackUpgradeChanged()
    {
        GetComponent<CharacterStackManager>().CheckUpgrade();
    }
}
