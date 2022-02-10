using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    public Joystick _joystick;
    public float moveSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(_joystick.Horizontal) > 0.1f || Mathf.Abs(_joystick.Vertical) > 0.1f)
        {
            JoystickMove(_joystick.Horizontal , _joystick.Vertical , transform , moveSpeed); 
            JoystickRotate(_joystick.Horizontal , _joystick.Vertical , transform );
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
