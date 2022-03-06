using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyMoveOnScreen1 : MonoBehaviour
{
    private Vector3 target;

    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("ME");
        transform.position = SceneReferences.instance.moneyTargetMoveImage.position;
        target = SceneReferences.instance.moneyMoveImage2Target;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position , target) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position , target , moveSpeed * Time.deltaTime);
        }
        else
        {
            //PlayerMoneyData.instance.TotalMoney += 20;
            Destroy(gameObject);
        }
    }
}
