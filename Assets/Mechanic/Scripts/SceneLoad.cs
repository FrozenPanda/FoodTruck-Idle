using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    private bool SceneLoadAlready = false;
    private void OnTriggerEnter(Collider other)
    {
        if (SceneLoadAlready)
        {
            return;
        }
        
        if (other.tag == "Scene")
        {
            Debug.Log("OtherScene" + other.gameObject.name);
            Destroy(other.gameObject);
            NextScene();
            SceneLoadAlready = true;
        }
    }

    private void NextScene()
    {
        
        int x = SceneManager.GetActiveScene().buildIndex;
        x++;
        x %= SceneManager.sceneCountInBuildSettings;
        //SceneManager.LoadScene(x);
        StartCoroutine(LoadYourAsyncScene(x));
        LoadingScreen.instance.gameObject.SetActive(true);
    }
    
    IEnumerator LoadYourAsyncScene(int sceneIndex)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        
    }
}
