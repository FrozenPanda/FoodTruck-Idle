using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyBoxContainer : MonoBehaviour
{
    public List<Transform> boxPlaces = new List<Transform>();
    public int BoxCount;
    public List<GameObject> Boxes = new List<GameObject>();
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform EmptyPlace()
    {
        return boxPlaces[BoxCount];
    }

    public bool IsThereBox()
    {
        return BoxCount > 0 ? true : false;
    }

    public void DeleteOneBox()
    {
        if (Boxes.Count > 0)
        {
            BoxCount--;
            Destroy(Boxes[Boxes.Count -1]);
            Boxes.RemoveAt(Boxes.Count -1);
        }
    }
}
