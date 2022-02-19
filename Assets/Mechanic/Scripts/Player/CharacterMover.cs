using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    public Joystick _joystick;
    public float moveSpeed;
    private float moveSpeedDefault;
    private AnimationController _animationController;
    void Start()
    {
        moveSpeedDefault = moveSpeed;
        _animationController = GetComponent<AnimationController>();
    }

    private bool _isMoving;

    public bool isMoving
    {
        get { return _isMoving; }
    }
    
    void Update()
    {
        if (Mathf.Abs(_joystick.Horizontal) > 0.1f || Mathf.Abs(_joystick.Vertical) > 0.1f)
        {
            JoystickMove(_joystick.Horizontal , _joystick.Vertical , transform , moveSpeed); 
            JoystickRotate(_joystick.Horizontal , _joystick.Vertical , transform );
            _animationController.playAnim(1);
            _isMoving = true;
        }
        else
        {
            _animationController.playAnim(0);
            _isMoving = false;
        }
        
        Debug.DrawRay(transform.position + transform.forward / 2f + Vector3.up *2f , Vector3.down *3f , Color.blue);
        
        RayForGround();
    }

    private RaycastHit Hit;
    public LayerMask groundAndObstacles;
    private void RayForGround()
    {
        
        if (Physics.Raycast(transform.position+ transform.forward / 2f + Vector3.up* 3f , Vector3.down , out Hit , 5f , groundAndObstacles))
        {
//            Debug.Log("RH + " + Hit.transform.name);
            
            if (Hit.transform.gameObject.layer == 6)
            {
                moveSpeed = moveSpeedDefault;
            }
            else
            {
                moveSpeed = 0f;
            }
        }
        else
        {
            Debug.Log("NoRH");
            
            moveSpeed = 0f;
        }
    }
    
    private void JoystickMove(float _hor , float _ver , Transform player , float _moveSpeed = 1f)
    {
        player.position += new Vector3(_hor * Time.deltaTime, 0f, _ver * Time.deltaTime) * _moveSpeed;
    }

    private void JoystickRotate(float _hor , float _ver , Transform player)
    {
        player.eulerAngles = new Vector3(0, Mathf.Atan2(_ver, -_hor) * 180 / Mathf.PI -90f, 0f);
    }
}
