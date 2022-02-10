using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDropable
{
    public void VereyimAbime(CharacterStackManager _characterStackManager);

    public Transform sayMyTransform();

    public bool isMealNeed();
}
