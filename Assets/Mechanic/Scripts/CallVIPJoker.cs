using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallVIPJoker : MonoBehaviour
{

    public float timerForActivate;
    private float timerForActivateSave;

    public float ActivateTimer;
    private float ActivateTimerSave;

    public GameObject ButtonHolder;
    public GameObject BeforeText;
    public Text AfterText;
    
    enum VIPbuttonEvent
    {
        Idle,
        CountDownForActivation,
        AvtiveNow
    }

    private VIPbuttonEvent _viPbuttonEvent;
    
    
    void Start()
    {
        ButtonHolder.SetActive(false);
        timerForActivateSave = timerForActivate;
        ActivateTimerSave = ActivateTimer;
        _viPbuttonEvent = VIPbuttonEvent.CountDownForActivation;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_viPbuttonEvent)
        {
            case VIPbuttonEvent.Idle:
                break;
            case VIPbuttonEvent.CountDownForActivation:

                if (timerForActivate > 0f)
                {
                    timerForActivate -= Time.deltaTime;
                }
                else
                {
                    ButtonHolder.SetActive(true);
                    BeforeText.SetActive(true);
                    AfterText.gameObject.SetActive(false);
                    _viPbuttonEvent = VIPbuttonEvent.Idle;
                    timerForActivate = timerForActivateSave;
                }
                
                break;
            case VIPbuttonEvent.AvtiveNow:

                if (ActivateTimer > 0f)
                {
                    ActivateTimer -= Time.deltaTime;
                    AfterText.text = ActivateTimer.ToString("F0");
                }
                else
                {
                    ButtonHolder.SetActive(false);
                    ActivateTimer = ActivateTimerSave;
                    _viPbuttonEvent = VIPbuttonEvent.CountDownForActivation;
                    AIcreator.instance.ViPalways = false;
                }
                
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void EnableBuff()
    {
        if (_viPbuttonEvent != VIPbuttonEvent.AvtiveNow)
        {
            _viPbuttonEvent = VIPbuttonEvent.AvtiveNow;
            BeforeText.SetActive(false);
            AfterText.gameObject.SetActive(true);
            AIcreator.instance.ViPalways = true;
        }
    }
}
