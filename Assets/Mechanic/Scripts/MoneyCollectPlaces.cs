using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class MoneyCollectPlaces : MonoBehaviour
{
    public int width;
    public int length;
    public int height;

    public GameObject model;
    public List<GameObject> allModels = new List<GameObject>();
    public List<MoneyStackable> currentFillPlaces = new List<MoneyStackable>();

    private float timer;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        
        Spawn();
        
        CloseMeshRender();
    }

    public enum MoneyStack
    {
        Idle,
        GivingMoney
    }

    public MoneyStack _moneyStackEvents;
    
    public void addMeToList(MoneyStackable _moneyStackable)
    {
        currentFillPlaces.Add(_moneyStackable);
    }

    private void Update()
    {
        switch (_moneyStackEvents)
        {
            case MoneyStack.Idle:
                break;
            case MoneyStack.GivingMoney:

                if (currentFillPlaces.Count < 1)
                {
                    return;
                }
                
                if (timer > 0f)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    CreateCoinImage(currentFillPlaces[currentFillPlaces.Count - 1].transform);
                    Destroy(currentFillPlaces[currentFillPlaces.Count - 1].gameObject);
                    currentFillPlaces.RemoveAt(currentFillPlaces.Count - 1);
                    timer = 0.05f;
                }
                
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public Transform takeLastElement()
    {
        return allModels[currentFillPlaces.Count].transform;
    }
    
    public void Clear()
    {
        for (int i = 0; i < allModels.Count; i++)
        {
            DestroyImmediate(allModels[i]);
        }
        
        allModels.Clear();
    }

    public void Spawn()
    {
        transform.eulerAngles = Vector3.zero;
        
        for (int i = 0; i < allModels.Count; i++)
        {
            if (allModels[0])
            {
                DestroyImmediate(allModels[0]);
            }
            allModels.RemoveAt(0);
        }
        
        allModels.Clear();

        Renderer _renderer = model.GetComponent<Renderer>();
        
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                for (int k = 0; k < length; k++)
                {
                    Vector3 newPos = transform.position + new Vector3(_renderer.bounds.size.x * k,
                        _renderer.bounds.size.y * i, _renderer.bounds.size.z * j);
                    GameObject go = Instantiate(model, newPos, model.transform.rotation, transform);
                    allModels.Add(go);
                }
            }
        }
    }

    public void CloseMeshRender()
    {
        for (int i = 0; i < allModels.Count; i++)
        {
            allModels[i].GetComponent<MeshRenderer>().enabled = false;
        }
    }
    
    public void EnableMeshRender()
    {
        for (int i = 0; i < allModels.Count; i++)
        {
            allModels[i].GetComponent<MeshRenderer>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _moneyStackEvents = MoneyStack.GivingMoney;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _moneyStackEvents = MoneyStack.Idle;
        }
    }

    private void CreateCoinImage(Transform _transform)
    {
        GameObject go = Instantiate(SceneReferences.instance.moneyMoveImage, SceneReferences.instance.moneyMoveCanvas);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(_transform.position);
        go.GetComponent<Image>().rectTransform.position = screenPos;
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(MoneyCollectPlaces))]

public class ControlSpawnEditor: Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MoneyCollectPlaces _objectSpawner = (MoneyCollectPlaces) target;

        if (GUILayout.Button("Spawn"))
        {
            _objectSpawner.Spawn();
        }

        if (GUILayout.Button("Clear"))
        {
            _objectSpawner.Clear();
        }
        
        if (GUILayout.Button("CloseMeshRenderer"))
        {
            _objectSpawner.CloseMeshRender();
        }
        
        if (GUILayout.Button("EnableMeshRenderer"))
        {
            _objectSpawner.EnableMeshRender();
        }
    }
}

#endif
