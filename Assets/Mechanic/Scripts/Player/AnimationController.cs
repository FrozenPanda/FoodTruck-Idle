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
    
    private void Start()
    {
        _characterStackManager = GetComponent<CharacterStackManager>();
    }

    //0 for idle, 1 for walk , 2 carry idle
    public void playAnim(int _id)
    {
        if (_id == 0)
        {
            if (_characterStackManager.isCharacterCarrying())
            {
                currentAnim = "idleC";
            }
            else
            {
                currentAnim = "idle";
            }
        }
        else
        {
            if (_characterStackManager.isCharacterCarrying())
            {
                currentAnim = "walkC";
            }
            else
            {
                currentAnim = "walk";
            }
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
        
        Debug.Log("AnimPlaying");
        
        anim.Play(playingAnim);
    }
    
    
}
