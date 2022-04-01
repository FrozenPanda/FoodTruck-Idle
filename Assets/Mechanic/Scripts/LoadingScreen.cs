using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitAndClose());
    }

    IEnumerator waitAndClose()
    {
        yield return new WaitForSeconds(1f);
        
        gameObject.SetActive(false);
    }
}
