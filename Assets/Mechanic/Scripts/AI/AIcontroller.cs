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
            gameObject.AddComponent<AItableEat>();
        }
    }

    public void SendAItoFinish()
    {
        _aIpathFollower.SetPathAndMove(exitPath , false);
        _creatableAI.CustomerEatAlready();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AIarrivedDestination()
    {
        GetComponent<AItableEat>().SetParameters(sandwichPos , _creatableAI);
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
