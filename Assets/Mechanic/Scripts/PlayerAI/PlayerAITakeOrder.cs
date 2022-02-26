using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAITakeOrder : MonoBehaviour
{
    //0 middle , 1 hotDogPlace
    public Transform[] MovePaths;
    public Transform[] MoveToHotDogPaths;
    public Transform[] MoveToMiddle;
    private int CurrentPathIndex = 0;
    public float moveSpeed;
    private AnimationController _animationController;
    public enum TakeOrderEvents
    {
        Idle,
        MovingToMiddle,
        MovingToHotDog,
        TakingHotDog,
        MovingMiddleWithOrder
    }

    public TakeOrderEvents _takeOrderEvents;
    
    private float hotDogtakeTimer;
    private CharacterStackManager _characterStackManager;
    
    private void Start()
    {
        _characterStackManager = GetComponent<CharacterStackManager>();
        _animationController = GetComponent<AnimationController>();
    }

    public void SendAItoTakeOrder()
    {
        transform.LookAt(MovePaths[0]);
        _takeOrderEvents = TakeOrderEvents.MovingToMiddle;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_takeOrderEvents)
        {
            case TakeOrderEvents.Idle:
                break;
            case TakeOrderEvents.MovingToMiddle:
                _animationController.playAnim(1, moveSpeed);
                transform.position = Vector3.MoveTowards(transform.position , MovePaths[0].position , moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position , MovePaths[0].position) < 0.1f)
                {
                    CurrentPathIndex = 0;
                    _takeOrderEvents = TakeOrderEvents.MovingToHotDog;
                    transform.LookAt(MovePaths[1]);
                }

                
                break;
            case TakeOrderEvents.MovingToHotDog:
                _animationController.playAnim(1, moveSpeed);
                transform.position = Vector3.MoveTowards(transform.position , MoveToHotDogPaths[CurrentPathIndex].position , moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position , MoveToHotDogPaths[CurrentPathIndex].position) < 0.1f)
                {
                    CurrentPathIndex++;
                    if (CurrentPathIndex >= MoveToHotDogPaths.Length)
                    {
                        _takeOrderEvents = TakeOrderEvents.TakingHotDog;
                    }
                    else
                    {
                        transform.LookAt(MoveToHotDogPaths[CurrentPathIndex]);
                    }
                }
                
                break;
            case TakeOrderEvents.TakingHotDog:
                _animationController.playAnim(0);
                if (_characterStackManager.IsCharacterCarryAtMax())
                {
                    CurrentPathIndex = 0;
                    _takeOrderEvents = TakeOrderEvents.MovingMiddleWithOrder;
                    transform.LookAt(MoveToMiddle[0]);
                }
                
                break;
            case TakeOrderEvents.MovingMiddleWithOrder:
                _animationController.playAnim(1, moveSpeed);
                transform.position = Vector3.MoveTowards(transform.position , MoveToMiddle[CurrentPathIndex].position , moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position , MoveToMiddle[CurrentPathIndex].position) < 0.1f)
                {
                    CurrentPathIndex++;
                    if (CurrentPathIndex >= MoveToMiddle.Length)
                    {
                        GetComponent<PlayerAIMove>().CharacterReadyWithOrder();
                        _takeOrderEvents = TakeOrderEvents.Idle;
                    }
                    else
                    {
                        transform.LookAt(MoveToMiddle[CurrentPathIndex]);
                    }
                }
                
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
