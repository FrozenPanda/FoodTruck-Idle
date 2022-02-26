using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator anim;

    private CharacterStackManager _characterStackManager;
    private string currentAnim;
    private string playingAnim;

    private float beforeWalkingSpeed;

    private void Start()
    {
        _characterStackManager = GetComponent<CharacterStackManager>();
    }

    //0 for idle, 1 for walk , 2 carry idle
    public void playAnim(int _id , float moveSpeed = 3f)
    {
        if (_id == 0)
        {
            currentAnim = "idle";
        }
        else
        {
            currentAnim = "walk";
            
        }
        
        if (_characterStackManager.isCharacterCarrying())
        {
            anim.SetLayerWeight(1,1);
        }
        else
        {
            anim.SetLayerWeight(1,0);
        }

        if (moveSpeed != beforeWalkingSpeed)
        {
            beforeWalkingSpeed = moveSpeed;
            anim.SetFloat("MoveSpeed" , moveSpeed / 3f);
        }
        
        playAnimWithName(currentAnim);
    }

    private void playAnimWithName(string _animName)
    {
        if (_animName == playingAnim)
        {
            return;
        }

        playingAnim = _animName;
        
        
        anim.Play(playingAnim);
    }
    
    
}
