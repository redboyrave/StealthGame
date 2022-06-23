using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Manager : MonoBehaviour

{
    [Header("Object References")]
    public Movement _movement;
    public WallHang _wallhang;
    public PlayerInputManager _input; 
    public Rigidbody rb;
    public PlayerInfo p_info;
    public Transform _camera_ref;
    public MakeNoise _noise;
    public CanClimbCheck _ledge;
    public Transform chest_pos;
    public CapsuleCollider stand_cap;
    public CapsuleCollider crouched_cap;
    public Animator _animator;


    [Header("Movement Variables")]
    [SerializeField]public float walk_speed = 5f;
    [SerializeField]public float crouch_speed = 2.5f;
    [SerializeField]public float dash_speed = 8f;
    [SerializeField]public float JumpHeight = 3f;
    [SerializeField]public float JumpTime = .5f;
    [SerializeField]public float JumpAceleration = .5f;

    [Header("Enemy Detection Variables")]
    [SerializeField]private float WalkingNoise;
    [SerializeField]private float CrouchModifier;
    [SerializeField]private float DashModifier;
    [SerializeField]private float JumpModifier;
    [SerializeField]private float LandingNoise;
    [SerializeField]private float HangingModifier;
    [HideInInspector]public float _noise_level = 0f;


// ANIMATION RELATED VARIABLES //
    [HideInInspector]public float vertical_speed;
    [HideInInspector]public float _move_speed = 0f;
    [HideInInspector]public bool is_grounded = false;
    [HideInInspector]public bool is_dashing = false;
    [HideInInspector]public bool is_crouched = false;
    [HideInInspector]public bool is_hanging = false;
    [HideInInspector]public float initial_jump_vel;
    [HideInInspector]public bool can_move_left = false;
    [HideInInspector]public bool can_move_right = false;
    [HideInInspector]public Quaternion last_rotation;



    void Start()
    {
        // _camera_ref = _input._camera;
        v0_jump_speed();
    }

    // Update is called once per frame
    void FixedUpdate(){
        ground_check();
        noise_check();
        RaycastHit forward_cast = raycast_forward(chest_pos.position);
        p_info.PlayerCurrentCoordinates = transform.position;
        _noise.Call_Noise(_noise_level);
        if(forward_cast.collider !=null){
            is_hanging = _ledge.IsGrabingLedge; 
        }
        if (is_hanging && forward_cast.collider !=null){
            start_hang();
        }
    }


    //Dash only works while holding the button, 
    //so we have a start and stop funcion to work with the input system
    //Dashing also cancels your crouch;
    public void start_dash(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        is_dashing = true;
        is_crouched = false;
    }
    public void stop_dash(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        is_dashing = false;

    }
    public void crouch(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        is_crouched = !is_crouched;
        if(is_crouched){
            char_crouch();
        }
        else{
            char_stand();
        }
    }

    public void start_movement(){
        last_rotation = transform.rotation;
        is_hanging = false;
        _movement.enabled=true;
        _wallhang.enabled=false;

        // _ledge.enabled=true;
    }
    public void start_hang(){
        last_rotation = transform.rotation;
        _movement.enabled=false;
        _wallhang.enabled=true;
        // _ledge.enabled=false;
    }
    private void ground_check(){
        // RaycastHit hit;
        // Physics.Raycast(transform.position,Vector3.down,out hit);
        // is_grounded = (transform.position.y - hit.point.y <=0.1);
        is_grounded = Physics.Raycast(transform.position,Vector3.down,.1f);

        //     Debug.Log("Grounded");
        //     is_grounded = true;
        // }
        // else{
        //     is_grounded=false;
        //     Debug.Log("Not Grounded");
        // }
    }
    private void v0_jump_speed(){
                    initial_jump_vel = JumpHeight/JumpTime - JumpAceleration*JumpTime/2f;
    }
    public void enable_move_coroutine(float seconds){
        StartCoroutine(enable_move(seconds));
    }
    IEnumerator enable_move(float seconds){
        Debug.Log("Starting to wait for " + seconds);
        yield return new WaitForSeconds(seconds);
        Debug.Log("Done Waiting");
        is_hanging=false;
        start_movement();
        }
    public RaycastHit raycast_forward(Vector3 from){
        RaycastHit[] hits = Physics.RaycastAll(from,transform.forward,5f);
        RaycastHit closest_valid_hit = new RaycastHit();
        foreach (RaycastHit hit in hits){
            if(hit.rigidbody == rb || hit.transform.IsChildOf(transform)){
                Debug.Log(hit.transform.name+" is either myself of a child of mine");
                continue;
            }
            if (closest_valid_hit.collider == null || closest_valid_hit.distance > hit.distance){
                closest_valid_hit = hit;
            }
            // || means a LOGICAL OR condition, good to know;
        }
        return closest_valid_hit;
    }
    public void char_stand(){
        stand_cap.enabled = true;
        crouched_cap.enabled =false;

        //OLD METHOD CHANGED THE SIZE OF THE COLLIDERS, NOW THESE VALUES ARE ONLY HERE FOR REFERENCE;
        // cap_col.height =1.82f;
        // cap_col.radius = 0.2f;
        // cap_col.center = new Vector3(0,0.9f,0);
        // // dist_to_ground = cap_col.bounds.extents.y - cap_col.center.y;
        // dist_to_ground = cap_col.height/2;
    }
    public void char_crouch(){
        stand_cap.enabled = false;
        crouched_cap.enabled = true;
        // cap_col.height = 1.22f;
        // cap_col.radius = 0.35f;
        // cap_col.center = new Vector3(0,0.6f,0);
        // // dist_to_ground = cap_col.bounds.extents.y - cap_col.center.y;
        // dist_to_ground = cap_col.height/2;
    }

    private void noise_check(){
        float noise = 0f;
        if(_move_speed!= 0){
            noise += WalkingNoise;
        if(is_crouched){
            noise *= CrouchModifier;
        }
        if(is_dashing){
           noise *= DashModifier;
        }
        if(!is_grounded){
            noise *= JumpModifier;
        }
        if(is_hanging){
            noise *= HangingModifier;
        }
        // if(is_landing;) // landing not implemented, there's currently no way of verifying
        }
        _noise_level = noise;
        // _noise_level = WalkingNoise * CrouchModifier * DashModifier * HangingModifier* JumpModifier + LandingNoise ;
    }
}