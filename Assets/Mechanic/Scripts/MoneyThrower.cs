using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyThrower : MonoBehaviour
{
    public float MoneyDropTimer;
    private float MoneyDropTimerSave;
    public int MoneyAmount;
    public MoneyCollectPlaces _moneyCollectPlaces;
    public GameObject moneyStackable;
    void Start()
    {
        MoneyDropTimerSave = MoneyDropTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (MoneyDropTimer > 0f)
        {
            MoneyDropTimer -= Time.deltaTime;
        }
        else
        {
            ThrowMoney();
        }
    }

    private void ThrowMoney()
    {
        MoneyDropTimer = MoneyDropTimerSave;

        StartCoroutine(DropMoneyWithDelay(MoneyAmount));
    }
    
    IEnumerator DropMoneyWithDelay(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            yield return new WaitForSeconds(0.05f);
            
            GameObject go = Instantiate(moneyStackable, transform.position + Vector3.up, Quaternion.identity);
            go.GetComponent<MoneyStackable>().StartMove(_moneyCollectPlaces);
        }
    }
}
