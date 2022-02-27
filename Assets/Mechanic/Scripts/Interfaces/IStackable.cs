using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStackable
{
    public void giveOnetoPlayer(CharacterStackManager _characterStackManager , int index , Transform target);

    public Transform sayMyTransform();

    public int MealIndex();
}
