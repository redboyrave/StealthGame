using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public Transform _camera;
    public Movement_Manager _movement_manager;
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
        _input.Player.Crouch.performed -= _movement_manager.crouch;
        _input.Player.Dash.performed -= _movement_manager.start_dash;
        _input.Player.Dash.canceled -= _movement_manager.stop_dash;
        _input.Disable();
    }
    void Start()
    {
       _camera =  _movement_manager._camera_ref;
        // Movement movement_script = GetComponent<Movement>();
        _input.Player.Crouch.performed += _movement_manager.crouch;
        _input.Player.Dash.performed += _movement_manager.start_dash;
        _input.Player.Dash.canceled += _movement_manager.stop_dash;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    JoystickInput = _input.Player.Move.ReadValue<Vector2>();
    Jump = (_input.Player.Jump.ReadValue<float>() == 1);
    }
}
