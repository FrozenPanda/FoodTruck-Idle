using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIcontroller : MonoBehaviour
{
    private AIpathFollower _aIpathFollower;
    private Transform enterPath;
    private Transform exitPath;
    private Transform sandwichPos;
    private ICreatableAI _creatableAI;

    private AItableEat _aItableEat;
    private AItruckEat _aItruckEat;
    
    public enum AIevent
    {
        TableEat,
        StandEat,
    }

    public AIevent _ievent;
    
    void Start()
    {
        
        //_aIpathFollower.afterPath = ActionTest;
    }

    public void SetAIAgent(Transform enterPath , Transform exitPath , AIevent _ievent ,ICreatableAI _creatableAI , Transform sandwichPlace = null)
    {
        this._creatableAI = _creatableAI;
        _aIpathFollower = GetComponent<AIpathFollower>();
        this.enterPath = enterPath;
        this.exitPath = exitPath;
        sandwichPos = sandwichPlace;
        _aIpathFollower.SetPathAndMove(this.enterPath);

        if (_ievent == AIevent.TableEat)
        {
           _aItableEat = gameObject.AddComponent<AItableEat>();
        }
    }

    public void SendAItoFinish()
    {
        _aIpathFollower.SetPathAndMove(exitPath , false);
        _creatableAI.CustomerEatAlready();

        if (_aItableEat)
        {
            GetComponent<AImoneyDrop>().StartMoneyDrop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject wantedCanvas;
    public void AIarrivedDestination()
    {
        if (_aItableEat)
        {
            //GetComponent<AItableEat>().SetParameters(sandwichPos , _creatableAI);
            _aItableEat.SetParameters(sandwichPos , _creatableAI);
            wantedCanvas.SetActive(true);
        }

        if (_aItruckEat)
        {
            
        }
    }

    public void AIfinished()
    {
        Debug.Log("AI destroyed");
        Destroy(gameObject);
    }

    public void OrderRequested()
    {
        
    }
}
