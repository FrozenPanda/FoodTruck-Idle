using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCreateForTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject moneyStackable;
    public MoneyCollectPlaces _moneyCollectPlaces;
    public int amount;
    
    void Start()
    {
        SaveLoadSystem.Load();

        if (SaveLoadSystem.instance.TotalMoney > 0)
        {
            gameObject.SetActive(false);
            return;
        }
    }

    public void EnableMoney()
    {
        if (SaveLoadSystem.instance.TotalMoney > 0)
        {
            gameObject.SetActive(false);
            return;
        }
        
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.1f);
        
        for (int i = 0; i < amount; i++)
        {
            GameObject go = Instantiate(moneyStackable, transform.position + Vector3.up, Quaternion.identity);
            go.GetComponent<MoneyStackable>().StartMove(_moneyCollectPlaces);
        }

        SaveLoadSystem.instance.MoneyCreateForTutorialBool = true;
        SaveLoadSystem.Save();
    }
}
