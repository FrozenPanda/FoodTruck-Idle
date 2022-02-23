using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStackManager : MonoBehaviour
{
    
    public List<Transform> StackPlaces = new List<Transform>();
    
    private int totalStack = 5; //todo bunu save load dan çekilmesi lazım
    public List<MoveAbleMeal> stackMeals = new List<MoveAbleMeal>();
    public int currentStack;

    public IStackable _stackable;
    private IDropable _dropable;
    private float collectTimer;
    private float givingTimer = 0.1f;

    private CharacterMover _characterMover;

    public int[] carryCapacityPerUpgrade;
    public float[] chargeSpeedPerUpgrade;

    public bool RealPlayer;
    
    private void Start()
    {
        _characterMover = GetComponent<CharacterMover>();
        SaveLoadSystem.Load();
        CheckUpgrade();
    }

    public enum StackEvent
    {
        Idle,
        Collecting,
        Giving
    }

    public StackEvent _stackEvent;
    
    private void Update()
    {
        switch (_stackEvent)
        {
            case StackEvent.Idle:
                hotDogTakeCanvas.SetActive(false);
                break;
            case StackEvent.Collecting:
                
                if (_stackable == null)
                {
                    Debug.Log("Stackable yok");
                    hotDogTakeCanvas.SetActive(false);
                    return;
                }
                
                if(Vector3.Distance(transform.position , colliderHitPoint) > 1f)
                {
                    _stackable = null;
                    IStackTransform = null;
                    _stackEvent = StackEvent.Idle;
                    
                    hotDogTakeCanvas.SetActive(false);
                }

                if (_characterMover.isMoving)
                {
                    collectTimer = defaultHotDogTime;
                    hotDogTakeCanvas.SetActive(false);
                    return;
                }
                else
                {
                    hotDogTakeCanvas.SetActive(true);
                }

                if (currentStack >= totalStack)
                {
                    Debug.Log("Idle a döndü");
                    CommonFunctions.instance.CreateAnyUI(SceneReferences.instance.HandFullImage , transform , SceneReferences.instance.moneyMoveCanvas);
                    _stackEvent = StackEvent.Idle;
                    hotDogTakeCanvas.SetActive(false);
                    return;
                }
                
                if (collectTimer > 0f)
                {
                    collectTimer -= Time.deltaTime;
                    SetCanvasFillBar(collectTimer);
                }
                else
                {
                    collectTimer = defaultHotDogTime;
                    if (currentStack < totalStack + 1)
                    {
                        _stackable.giveOnetoPlayer(this, currentStack , StackPlaces[currentStack]);
                        //currentStack++;
                        carryingBool = true;
                    }
                }
                
                break;
            case StackEvent.Giving:

                if (_dropable == null)
                {
                    return;
                }
                
                if (Vector3.Distance(transform.position , IdropTransform.position) > 3f)
                {
                    _dropable = null;
                    IdropTransform = null;
                    _stackEvent = StackEvent.Idle;
                }

                if (givingTimer > 0f)
                {
                    givingTimer -= Time.deltaTime;
                }
                else
                {
                    givingTimer = 0.1f;
                    if (currentStack > 0)
                    {
                        if (_dropable == null)
                        {
                            return;
                        }
                        
                        if (_dropable.isMealNeed())
                        {
                            _dropable.VereyimAbime(this);

                            if (RealPlayer)
                            {
                                if (PlayerAIMove.instance)
                                {
                                    PlayerAIMove.instance.CheckIfOrderStillNeed();
                                }
                            }
                        }
                    }
                }
                
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private Transform IStackTransform;
    private Transform IdropTransform;
    private Vector3 colliderHitPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IStackable>() != null)
        {
            colliderHitPoint = transform.position;
            _stackable = other.GetComponent<IStackable>();
            collectTimer = defaultHotDogTime;
            _stackEvent = StackEvent.Collecting;
            Debug.Log("Stackable var");
            
            IStackTransform = _stackable.sayMyTransform();
            hotDogTakeCanvas.SetActive(true);
        }

        if (other.GetComponent<IDropable>() != null)
        {
            _dropable = other.GetComponent<IDropable>();
            _stackEvent = StackEvent.Giving;
            IdropTransform = _dropable.sayMyTransform();
            Debug.Log("Droppable var");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        return;
        if (other.tag == "CollectPlace")
        {
            Debug.Log("Ontriggerexit");
            _stackable = null;
            IStackTransform = null;
            _stackEvent = StackEvent.Idle;
            hotDogTakeCanvas.SetActive(false);
        }
    }


    public void mealTaken(int index , MoveAbleMeal _moveAbleMeal)
    {
        stackMeals[index] = _moveAbleMeal;
    }

    public void mealGiven(Transform mealTarget)
    {
        MoveAbleMeal _moveAbleMeal;

        for (int i = stackMeals.Count - 1; i < stackMeals.Count; i--)
        {
            if (stackMeals[i] != null)
            {
                _moveAbleMeal = stackMeals[i];
                Destroy(_moveAbleMeal.gameObject, 2f);
                stackMeals[i] = null;
                currentStack--;
                _moveAbleMeal.StartMove(mealTarget, MoveAbleMeal.moveEvent.ToTable);
                break;
            }
        }

        if (currentStack == 0)
        {
            carryingBool = false;
        }
    }

    private bool carryingBool;
    public bool isCharacterCarrying()
    {
        return carryingBool;
    }

    //hotdog take canvas
    public GameObject hotDogTakeCanvas;
    public Image hotDogTakeFillImage;
    private float defaultHotDogTime = 1f;
    private float currentHotDogTimer;

    private void SetCanvasFillBar(float currentTime)
    {
        hotDogTakeFillImage.fillAmount = 1 -(currentTime / defaultHotDogTime);
    }

    public void CheckUpgrade()
    {
        SaveLoadSystem.Load();
        totalStack = carryCapacityPerUpgrade[SaveLoadSystem.instance.upgrades2[0]];
        defaultHotDogTime = chargeSpeedPerUpgrade[SaveLoadSystem.instance.upgrades2[1]];
    }
}
