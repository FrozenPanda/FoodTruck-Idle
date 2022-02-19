using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTest : MonoBehaviour
{
    // Start is called before the first frame updat
    public int index;
    public int newValue;
    void Start()
    {
        SaveLoadSystem.Load();
        Debug.Log("Current Upgrade: " + SaveLoadSystem.instance.CharacterCarryCapacity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveLoadSystem.instance.TableSeatAmount = 3;
            SaveLoadSystem.Save();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            SaveLoadSystem.instance.CharacterCarryCapacity = 2;
            SaveLoadSystem.Save();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            
        }
    }
}
