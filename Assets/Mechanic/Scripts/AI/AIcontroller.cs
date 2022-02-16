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

    public void SetAIAgent(Transform enterPath , Transform exitPath , AIevent _ievent ,ICreatableAI _creatableAI = null , Transform sandwichPlace = null)
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

    public void SetAIAgent(Transform enterPath, Transform exitPath, AIevent _aIevent)
    {
        _aIpathFollower = GetComponent<AIpathFollower>();
        this.enterPath = enterPath;
        this.exitPath = exitPath;
        _aIpathFollower.SetPathAndMove(this.enterPath);
        _aItruckEat = gameObject.AddComponent<AItruckEat>();
        this._ievent = _aIevent;
    }

    public void SendAItoFinish()
    {
        Debug.Log("SendAItoFinish");
        
        _aIpathFollower.SetPathAndMove(exitPath , false);
        
        GetComponent<AImoneyDrop>().StartMoneyDrop();
        
        if (_aItableEat)
        {
            GetComponent<AImoneyDrop>().StartMoneyDrop();
            _creatableAI.CustomerEatAlready();
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
            GetComponent<AIanimController>().playAnimWithName("sit");
        }

        if (_aItruckEat)
        {
            _aItruckEat.SetParamatersAndGo();
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
