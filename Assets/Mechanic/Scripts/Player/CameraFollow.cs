using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Quaternion defaultRot;
    private Transform _cam;
    private Vector3 offSet;

    private Vector3 lastPos;
    private Quaternion lastRot;

    public Transform hotDogLookPlace;

    private float LookTimer = 0f;

    public static CameraFollow instance;

    private void Awake()
    {
        instance = this;
    }

    public enum CamEvent
    {
        CharFollow,
        GoToCharacter,
        GoToHogLine
    }

    private CamEvent _camEvent;

    public void LookHotDogPlace()
    {
        lastPos = _cam.position;
        lastRot = _cam.rotation;
        _camEvent = CamEvent.GoToHogLine;
    }

    private void LookCharacter()
    {
        LookTimer = 0f;
        lastPos = _cam.position;
        lastRot = _cam.rotation;
        _camEvent = CamEvent.GoToCharacter;
    }
    
    void Start()
    {
        _cam = Camera.main.transform;
        offSet = _cam.position - transform.position;
        defaultRot = _cam.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_camEvent)
        {
            case CamEvent.CharFollow:
                _cam.position = transform.position + offSet;
                break;
            case CamEvent.GoToCharacter:

                LookTimer += Time.deltaTime;
                _cam.position = Vector3.Lerp(lastPos , transform.position + offSet , LookTimer);
                _cam.rotation = Quaternion.Lerp(lastRot , defaultRot , LookTimer);

                if (LookTimer > 1f)
                {
                    LookTimer = 0f;
                    _camEvent = CamEvent.CharFollow;
                }
                
                break;
            case CamEvent.GoToHogLine:

                LookTimer += Time.deltaTime;
                _cam.position = Vector3.Lerp(lastPos , hotDogLookPlace.position , LookTimer);
                _cam.rotation = Quaternion.Lerp(lastRot , hotDogLookPlace.rotation , LookTimer);

                if (LookTimer > 2.5f)
                {
                    LookCharacter();
                }
                
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
