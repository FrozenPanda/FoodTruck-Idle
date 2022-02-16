using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyMoveOnScreen : MonoBehaviour
{
    private Transform target;

    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        target = SceneReferences.instance.moneyTargetMoveImage;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position , target.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position , target.position , moveSpeed * Time.deltaTime);
        }
        else
        {
            SceneReferences.instance.CurrentMoneyAmount += 20;
            SceneReferences.instance.TotalMoneyText.text = SceneReferences.instance.CurrentMoneyAmount.ToString();
            Destroy(gameObject);
        }
    }
}
