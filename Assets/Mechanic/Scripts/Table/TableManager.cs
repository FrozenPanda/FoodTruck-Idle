using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour , IDropable
{
    public GameObject[] Seats;
    
    //-1 is not unlocked yet -- 0 is empty -- 1 is waiting to come -- 2 is customerCame -- 3meal given
    private int[] seatsCondition = new[] {-1, -1, -1, -1};

    public Transform[] sandwichPlace;

    private void OnEnable()
    {
        StartFromSaveLoadSystem();
    }

    public void StartFromSaveLoadSystem()
    {
        SaveLoadSystem.Load();
        int unlockedSeat = SaveLoadSystem.instance.TableSeatAmount;
        for (int i = 0; i < Seats.Length; i++)
        {
            if (i < unlockedSeat)
            {
                Seats[i].SetActive(true);
                seatsCondition[i] = 0;
                AIcreator.instance.AddMeTolist(Seats[i].GetComponent<ICreatableAI>());
            }
            else
            {
                Seats[i].SetActive(false);
            }

            Seats[i].GetComponent<SeatData>().SeatID = i;
            Seats[i].GetComponent<SeatData>()._tableManager = this;
        }
        
        CheckSeatsAtBeginnging();
    }

    private void CheckSeatsAtBeginnging()
    {
        
    }

    public void CustomerEatCallNextOne(int seatID)
    {
        AIcreator.instance.AddMeTolist(Seats[seatID].GetComponent<ICreatableAI>());
        seatsCondition[seatID] = 1;
    }

    public void OrderRequested(int ID)
    {
        seatsCondition[ID] = 2;
    }

    public void mealEaten(int ID)
    {
        CustomerEatCallNextOne(ID);
    }

    public void VereyimAbime(CharacterStackManager _characterStackManager)
    {
        _characterStackManager.mealGiven(Seats[currentIndex].GetComponent<SeatData>().sandwichPlace);
    }

    public Transform sayMyTransform()
    {
        return transform;
    }

    private int currentIndex;
    public bool isMealNeed()
    {
        currentIndex = -1;
        for (int i = 0; i < seatsCondition.Length; i++)
        {
            if (seatsCondition[i] == 2)
            {
                currentIndex = i;
                Seats[i].GetComponent<SeatData>().currentAI.GetComponent<AItableEat>().StartEating();
                seatsCondition[i] = 3;
                return true;
            }
        }

        return false;
    }
}
