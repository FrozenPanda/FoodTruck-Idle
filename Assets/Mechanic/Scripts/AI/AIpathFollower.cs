using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIpathFollower : MonoBehaviour
{
    public enum pathEvent
    {
        Idle,
        Walking,
        End
    }

    public pathEvent _pathEvent;

    private Transform[] pathes;
    private int currentPathIndex;

    public Transform test;

    public Action afterPathTarget;
    public Action afterPathCompleted;

    private AIanimController _aIanimController;

    private void Awake()
    {
        _aIanimController = GetComponent<AIanimController>();
    }

    private void Start()
    {
        
        
        if (test)
        {
            SetPathAndMove(test);
        }
    }

    private bool entranceBool;
    public void SetPathAndMove(Transform pathParent, bool entrance = true)
    {
        currentPathIndex = 0;
        
        pathes = new Transform[pathParent.childCount];

        for (int i = 0; i < pathes.Length; i++)
        {
            pathes[i] = pathParent.GetChild(i);
        }

        _pathEvent = pathEvent.Walking;
        
        transform.LookAt(pathes[currentPathIndex]);

        entranceBool = entrance;
        
        _aIanimController.playAnimWithName("walk");
    }

    private void Update()
    {
        switch (_pathEvent)
        {
            case pathEvent.Idle:
                break;
            case pathEvent.Walking:
                
                transform.position = Vector3.MoveTowards(transform.position , pathes[currentPathIndex].position , 3f * Time.deltaTime);

                if (Vector3.Distance(transform.position , pathes[currentPathIndex].position) < 0.1f)
                {
                    NextPath();    
                }
                
                break;
            case pathEvent.End:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void NextPath()
    {
        if (currentPathIndex < pathes.Length - 1)
        {
            currentPathIndex++;
            transform.LookAt(pathes[currentPathIndex]);
        }
        else
        {
            Debug.Log("End Of The Path");
            if (entranceBool)
            {
                GetComponent<AIcontroller>().AIarrivedDestination();
                
            }
            else
            {
                GetComponent<AIcontroller>().AIfinished();
            }
            _pathEvent = pathEvent.End;
        }
    }
}
