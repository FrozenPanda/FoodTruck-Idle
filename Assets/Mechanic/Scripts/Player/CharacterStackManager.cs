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

    private CharacterMover _characterMover;

    private void Start()
    {
        _characterMover = GetComponent<CharacterMover>();
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
                break;
            case StackEvent.Collecting:
                
                if (_stackable == null)
                {
                    Debug.Log("Stackable yok");
                    return;
                }
                
                /*if (Vector3.Distance(transform.position , IStackTransform.position) > 2f)
                {
                    _stackable = null;
                    IStackTransform = null;
                    _stackEvent = StackEvent.Idle;
                    hotDogTakeCanvas.SetActive(false);
                }*/

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
                    if (currentStack < totalStack)
                    {
                        _stackable.giveOnetoPlayer(this, currentStack , StackPlaces[currentStack]);

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

                if (collectTimer > 0f)
                {
                    collectTimer -= Time.deltaTime;
                }
                else
                {
                    collectTimer = 0.1f;
                    if (currentStack > 0)
                    {
                        if (_dropable.isMealNeed())
                        {
                            _dropable.VereyimAbime(this);
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IStackable>() != null)
        {
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "CollectPlace")
        {
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
}
