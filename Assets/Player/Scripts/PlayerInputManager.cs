using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public Transform _camera;
    [HideInInspector]public Vector2 JoystickInput;
    [HideInInspector]public bool Jump; 
    private InputMaster _input;
    void Awake(){
        _input = new InputMaster();
    }

    void OnEnable(){
        _input.Enable();
    }
    void OnDisable(){
        _input.Disable();
    }
    void Start()
    {
        _input.Player.Crouch.performed += GetComponent<Movement>().crouch;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    JoystickInput = _input.Player.Move.ReadValue<Vector2>();
    Jump = _input.Player.Jump.ReadValue<float>() == 1;
    }
}
