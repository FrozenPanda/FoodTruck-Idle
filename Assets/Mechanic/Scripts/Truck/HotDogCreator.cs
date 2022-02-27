using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotDogCreator : MonoBehaviour , IStackable
{
    public Transform hotDogCreatePos;
    public List<Transform> HotDogPlaces = new List<Transform>();
    
    //0 for empty 1 for available
    public List<int> HotDogPlaceInfo = new List<int>();
    public List<MoveAbleMeal> waitingMeals = new List<MoveAbleMeal>();

    public GameObject HotDog;
    private float hotDogCookTimer;

    public MoveAbleMeal.MealType _mealType;
    void Update()
    {
        if (hotDogCookTimer > 0f)
        {
            hotDogCookTimer -= Time.deltaTime;
        }
        else
        {
            createHotDog();
            hotDogCookTimer = 2f;
        }
    }

    void createHotDog()
    {
        Transform target = CheckForEmptySpace();
        
        if (!target)
        {
            return;
        }
        
        GameObject go = Instantiate(HotDog, hotDogCreatePos.position, Quaternion.identity);
        MoveAbleMeal _moveAbleMeal = go.GetComponent<MoveAbleMeal>();
        _moveAbleMeal._mealType = this._mealType;
        _moveAbleMeal.StartMove(target, MoveAbleMeal.moveEvent.ToCookPlace);
        waitingMeals[currentPlace] = go.GetComponent<MoveAbleMeal>();
    }

    private int currentPlace;
    private Transform CheckForEmptySpace()
    {
        bool empty = false;

        for (int i = 0; i < HotDogPlaceInfo.Count; i++)
        {
            if (HotDogPlaceInfo[i] == 0)
            {
                HotDogPlaceInfo[i] = 1;
                currentPlace = i;
                return HotDogPlaces[i];
            }
        }

        return null;
    }

    public void giveOnetoPlayer(CharacterStackManager _characterStackManager , int currentStack , Transform target)
    {
        int firstHotDog = -1;

        for (int i = 0; i < HotDogPlaceInfo.Count; i++)
        {
            if (HotDogPlaceInfo[i] == 1)
            {
                firstHotDog = i;
                break;
            }
        }

        if (firstHotDog == -1)
        {
            return;
        }
        
        _characterStackManager.mealTaken(currentStack, waitingMeals[firstHotDog]);
        waitingMeals[firstHotDog].StartMove(target , MoveAbleMeal.moveEvent.ToPlayer);
        _characterStackManager.currentStack++;
        HotDogPlaceInfo[firstHotDog] = 0;
    }

    public Transform sayMyTransform()
    {
        return transform;
    }
}
