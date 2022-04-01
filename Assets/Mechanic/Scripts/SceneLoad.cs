using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Scene")
        {
            NextScene();    
        }
    }

    private void NextScene()
    {
        int x = SceneManager.GetActiveScene().buildIndex;
        x++;
        x %= SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(x);
    }
}
