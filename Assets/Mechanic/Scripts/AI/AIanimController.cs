using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIanimController : MonoBehaviour
{
    public Animator anim;

    private string playingAnim;
    
    public void playAnimWithName(string _animName , float speed = 3)
    {
        if (_animName == playingAnim)
        {
            return;
        }
        
        anim.SetFloat("MoveSpeed" , speed/3f);

        playingAnim = _animName;
        
//        Debug.Log("AnimPlaying");
        
        anim.Play(playingAnim);
    }
}
