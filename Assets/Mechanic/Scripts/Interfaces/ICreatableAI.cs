using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICreatableAI
{
    public void SendData();

    public void CustomerEatAlready();

    public void CustomerOrderRequested();

    public MoneyCollectPlaces GiveMeMoneyCollectPlaceData();

    public int GetSeatID();

    public int GetBotIndex();

    public HotDogQueuManager sendMeHotDogQueuManagerData();

    public bool IsThereAnyMeal();
}
