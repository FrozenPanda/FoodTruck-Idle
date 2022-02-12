using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIanimController : MonoBehaviour
{
    public Animator anim;

    private string playingAnim;
    
    public void playAnimWithName(string _animName)
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
