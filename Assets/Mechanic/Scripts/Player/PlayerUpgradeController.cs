using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUpgradeController : MonoBehaviour
{
    public static PlayerUpgradeController instance;

    public Transform upgradeInstantiateCanvas;
    public bool playerAI;
    private void Awake()
    {
        if (playerAI)
        {
            return;
        }
        
        instance = this;
    }

    public void characterStackUpgradeChanged()
    {
        GetComponent<CharacterStackManager>().CheckUpgrade();
    }

    public GameObject UpgradesOnPlayer;

    public void InstantiatePrefabsOnPlayer(int id)
    {
        GameObject go = Instantiate(UpgradesOnPlayer , upgradeInstantiateCanvas);
        string _text = "Null";
        if (id == 0)
        {
            _text = "Your Capacity +1";
        }else if (id == 1)
        {
            _text = "Your Charge Speed + 10%";
        }else if (id == 2)
        {
            _text = "Customer Eat Speed +10%";
        }else if (id == 3)
        {
            _text = "Customer Move Speed +10%";
        }else if (id == 4)
        {
            _text = "VIP tip increased";
        }else if (id == 5)
        {
            _text = "Stuff Capacity +1";
        }else if (id == 6)
        {
            _text = "Stuff Speed +10%";
        }

        go.transform.GetChild(0).GetComponent<Text>().text = _text;
    }

    public void InstantiatePrefabsOnLine()
    {
        CameraFollow.instance.LookHotDogPlace();

        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        
        GameObject go = Instantiate(UpgradesOnPlayer , upgradeInstantiateCanvas);
        string _text = "Null";
        _text = "Hot Dog Line Capacity +1";
        go.transform.GetChild(0).GetComponent<Text>().text = _text;
    }
}
