using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AImoneyDrop : MonoBehaviour
{
    public GameObject moneyToDrop;
    public GameObject moneyStackable;
    public int dropAmount = 3;

    public void StartMoneyDrop(int _amount , bool stackable = false , ICreatableAI _creatableAI = null)
    {
        if (!stackable)
        {
            for (int i = 0; i < _amount; i++)
            {
                GameObject go = Instantiate(moneyToDrop, transform.position + Vector3.up, Quaternion.identity);
            }
        }
        else
        {
            StartCoroutine(DropMoneyWithDelay(_amount, _creatableAI));
        }
    }

    IEnumerator DropMoneyWithDelay(int amount , ICreatableAI _creatableAI)
    {
        for (int i = 0; i < amount; i++)
        {
            yield return new WaitForSeconds(0.05f);
            
            GameObject go = Instantiate(moneyStackable, transform.position + Vector3.up, Quaternion.identity);
            go.GetComponent<MoneyStackable>().StartMove(_creatableAI);
        }
    }
}
