using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHang : MonoBehaviour
{
    [Header("MOVEMENT MANAGER")]
    [SerializeField]private Movement_Manager _movement_manager;
    [Header("Wall Position Offset")]
    [SerializeField]private float forward_offset = 0f;
    [SerializeField]private float up_offset = 0f;
    [Header("Edge Limit")]
    [SerializeField]private Transform LeftEdge;
    [SerializeField]private Transform CenterEdge;
    [SerializeField]private Transform RightEdge;

    private float turn_velocity;
    // [SerializeField]private PlayerInputManager _input;
    // [SerializeField]private Transform head_pos;
    // [SerializeField]private Animator _animator;
    // private Rigidbody rb;
    // private Transform _camera_ref;
    // private MakeNoise _noise;
    // [HideInInspector]public float vertical_speed;
    // [HideInInspector]public bool is_grounded = false;
    // [HideInInspector]public bool is_dashing = false;
    // [HideInInspector]public bool is_crouched = false;
    // [HideInInspector]public bool is_hanging = true;


    // Start is called before the first frame update
    void OnEnable(){
        Debug.Log("Wallhang Enabled");
        _movement_manager.rb.isKinematic=true;
        _movement_manager._animator.applyRootMotion=true;
        set_hang();
        wall_hang();
    }
    void OnDisable(){
        Debug.Log("WallhangDisable");
        _movement_manager._animator.applyRootMotion = false;
        _movement_manager.rb.isKinematic=false;
    }
    void FixedUpdate(){
        // rotate_toward_wall();
        // StartCoroutine(rotate_to_wall_());
        wall_hang();
        limit_movement();
    }

    void set_hang(){
        _movement_manager.rb.isKinematic = true;
        _movement_manager._animator.applyRootMotion = true;
        transform.position += this.transform.forward * forward_offset;
        transform.position += this.transform.up * up_offset;
        rotate_toward_wall();
    }
    void wall_hang(){
        _movement_manager.vertical_speed = 0;
        _movement_manager.is_hanging = true;
    }

    IEnumerator rotate_to_wall_(){
        // float t =0f;
        // t += Time.deltaTime;
        RaycastHit wallhit = _movement_manager.raycast_forward(_movement_manager.chest_pos.position);
        Vector3 walldir = -wallhit.normal;
        // float angle =Vector3.Angle(walldir,Vector3.forward);
        // Quaternion rotation = Quaternion.AngleAxis(angle,Vector3.up);
        Quaternion rotation = Quaternion.LookRotation(walldir,Vector3.up);
        // Quaternion rotation = Quaternion.Euler(0,angle,0);
        for (float t = 0f; t <= 1f; t += Time.deltaTime){
            _movement_manager.rb.rotation = Quaternion.Slerp(transform.rotation,rotation,t);
            Debug.Log(rotation);
            yield return null;;
        }

    }
    
    void rotate_toward_wall(){
        float t = 0 ;
        t += Time.deltaTime;
        RaycastHit wall_hit = _movement_manager.raycast_forward(_movement_manager.chest_pos.position);
        Debug.Log(wall_hit.normal);
        Vector3 wallnormal = wall_hit.normal;
        // float targetAngle = Mathf.Atan2(wallnormal.x,wallnormal.z)*Mathf.Rad2Deg;
        // float angle = Mathf.SmoothDampAngle(transform.rotation.eulerAngles.y,targetAngle,ref turn_velocity,Time.deltaTime);
        // Quaternion rotation = Quaternion.AngleAxis(angle,Vector3.up);
        // Quaternion rot = Quaternion.Lerp(transform.rotation,rotation,t);
        Quaternion rot = Quaternion.LookRotation(-wallnormal,Vector3.up);
        transform.rotation = rot;

        // Debug.Log(rot.normalized);
        // _movement_manager.rb.rotation = rot;
    }
    void limit_movement(){
        RaycastHit lefthitdata;
        RaycastHit centerhitdata;
        RaycastHit righthitdata;
        bool left_hit = Physics.Raycast(LeftEdge.position,Vector3.down,out lefthitdata,1f);
        bool center_hit = Physics.Raycast(CenterEdge.position,Vector3.down,out centerhitdata,1f);
        bool right_hit = Physics.Raycast(RightEdge.position,Vector3.down,out righthitdata,1f);

        if(center_hit){
            if (left_hit){
                //Can move left;
                _movement_manager.can_move_left = true;
            }
            else{
                _movement_manager.can_move_left = false;
            }
            if (right_hit){
                //Can move right;
                _movement_manager.can_move_right = true;
            }
            else{
                _movement_manager.can_move_right = false;
            }
        }

    }
}
