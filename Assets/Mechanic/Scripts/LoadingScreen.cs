using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public Text LoadingText;
    void Start()
    {
        StartCoroutine(waitAndClose());
    }

    IEnumerator waitAndClose()
    {
        LoadingText.text = "Loading.";

        yield return new WaitForSeconds(0.2f);
        
        LoadingText.text = "Loading..";

        yield return new WaitForSeconds(0.2f);
        
        LoadingText.text = "Loading...";

        yield return new WaitForSeconds(0.2f);
        
        LoadingText.text = "Loading.";

        yield return new WaitForSeconds(0.2f);
        
        LoadingText.text = "Loading..";

        yield return new WaitForSeconds(0.2f);
        
        LoadingText.text = "Loading...";

        yield return new WaitForSeconds(0.2f);

        gameObject.SetActive(false);
    }
}
