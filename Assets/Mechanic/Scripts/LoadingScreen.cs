using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public Text LoadingText;
    public static LoadingScreen instance;
    private void Awake()
    {
        instance = this;
    }

    private enum Loading
    {
        Load1,
        Load2,
        Load3
    }

    private Loading _loading;
    
    void Start()
    {
        gameObject.SetActive(false);
        LoadingText.text = "Loading.  ";
        //StartCoroutine(waitAndClose());
    }

    private float timer = 0.2f;
    private void Update()
    {
        switch (_loading)
        {
            case Loading.Load1:

                if (timer > 0f)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    _loading = Loading.Load2;
                    timer = 0.2f;
                }
                
                break;
            case Loading.Load2:
                
                if (timer > 0f)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    _loading = Loading.Load3;
                    timer = 0.2f;
                    LoadingText.text = "Loading.. ";
                }
                
                break;
            case Loading.Load3:
                
                if (timer > 0f)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    _loading = Loading.Load1;
                    timer = 0.2f;
                    LoadingText.text = "Loading...";
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    IEnumerator waitAndClose()
    {
        LoadingText.text = "Loading.  ";

        yield return new WaitForSeconds(0.2f);
        
        LoadingText.text = "Loading.. ";

        yield return new WaitForSeconds(0.2f);
        
        LoadingText.text = "Loading...";

        yield return new WaitForSeconds(0.2f);
        
        LoadingText.text = "Loading.  ";

        yield return new WaitForSeconds(0.2f);
        
        LoadingText.text = "Loading.. ";

        yield return new WaitForSeconds(0.2f);
        
        LoadingText.text = "Loading...";

        yield return new WaitForSeconds(0.2f);

        gameObject.SetActive(false);
    }
}
