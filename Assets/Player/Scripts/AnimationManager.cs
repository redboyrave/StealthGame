using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Movement_Manager mvt_mng;
    // private Movement mvt;
    // private WallHang wh;
    public Animator anim;
    public PlayerInputManager _input;

    private float movement_speed;
    private float vertical_speed;
    private bool is_crouched;
    private bool is_hanging;
    private bool is_grounded;
    private bool is_dashing;
    private float x_input;
    private float y_input;
    private bool can_move_left;
    private bool can_move_right;

    private Component current_enabled;

    //HASH for animator
    private int movement_speed_hash;
    private int vertical_speed_hash;
    private int is_grounded_hash;
    private int is_crouching_hash;
    private int is_dashing_hash;
    private int is_hanging_hash;
    private int x_input_hash;
    private int y_input_hash;
    private int can_move_left_hash;
    private int can_move_right_hash;


    // Start is called before the first frame update
    void Start()
    {   //Initialize hashes
        movement_speed_hash = make_hash("movement_speed");
        vertical_speed_hash = make_hash("vertical_speed");
        is_grounded_hash = make_hash("is_grounded");
        is_crouching_hash = make_hash("is_crouched");
        is_dashing_hash = make_hash("is_dashing");
        is_hanging_hash = make_hash("is_hanging"); 
        x_input_hash = make_hash("x_input");
        y_input_hash = make_hash("y_input");
        can_move_left_hash = make_hash("can_move_left");
        can_move_right_hash = make_hash("can_move_right");


    }

    void Update()
    {   
        movement_speed = mvt_mng._move_speed;
        vertical_speed = mvt_mng.vertical_speed;
        is_grounded = mvt_mng.is_grounded;
        is_crouched = mvt_mng.is_crouched;
        is_dashing = mvt_mng.is_dashing;
        is_hanging = mvt_mng.is_hanging;
        x_input = _input.JoystickInput.x;
        y_input = _input.JoystickInput.y;
        can_move_left = mvt_mng.can_move_left;
        can_move_right = mvt_mng.can_move_right;

        update_animator();
        

    }

    void update_animator(){
        anim.SetFloat(movement_speed_hash,movement_speed);
        anim.SetFloat(vertical_speed_hash,vertical_speed);
        anim.SetBool(is_grounded_hash,is_grounded);
        anim.SetBool(is_crouching_hash,is_crouched);
        anim.SetBool(is_dashing_hash,is_dashing);
        anim.SetBool(is_hanging_hash,is_hanging);
        anim.SetFloat(x_input_hash,x_input);
        anim.SetFloat(y_input_hash,y_input);
        anim.SetBool(can_move_left_hash,can_move_left);
        anim.SetBool(can_move_right_hash,can_move_right);
    }
    private int make_hash(string str){
        return Animator.StringToHash(str);
    }
}
//     void OnAnimatorMove(){
//         if (anim){
//             Vector3 offset = transform.position;
//             offset.x += anim.deltaPosition.x * XZPlaneOffset;
//             offset.z += anim.deltaPosition.z * XZPlaneOffset;
//             offset.y += anim.deltaPosition.y * YOffset;
//             transform.position = offset;
//         }
//     }
//     void OnStateEnter(AnimatorStateInfo state){
//         Debug.Log(state.fullPathHash);
//         if(state.IsName("HangingIdle")){
//             Debug.Log("HangingIdle");
//             HangIK_on();
//         if(state.IsName("Hanging-Drop")){
//             Debug.Log("Hanging-Drop");
//             HangIk_off();
//             mvt_mng.start_movement();   

//         }

//         }

//     }
//     void OnStateExit(AnimatorStateInfo state){
//         if (state.IsName("HangingClimb-Crouched")){
//             HangIk_off();
//             mvt_mng.start_movement();
//         if (state.IsName("Hanging-JumpBack")){
//             HangIk_off();
//             mvt_mng.start_movement();
//         }
//         }
//     }
//     void HangIK_on(){
//             anim.SetIKPosition(AvatarIKGoal.LeftHand,LeftHand.position);
//             anim.SetIKPosition(AvatarIKGoal.RightHand,RightHand.position);
//             anim.SetIKPositionWeight(AvatarIKGoal.LeftHand,1f);
//             anim.SetIKPositionWeight(AvatarIKGoal.RightHand,1f);
//     }
//     void HangIk_off(){
//             anim.SetIKPosition(AvatarIKGoal.LeftHand,LeftHand.position);
//             anim.SetIKPosition(AvatarIKGoal.RightHand,RightHand.position);
//             anim.SetIKPositionWeight(AvatarIKGoal.LeftHand,0f);
//             anim.SetIKPositionWeight(AvatarIKGoal.RightHand,0f);
//     }
// }
