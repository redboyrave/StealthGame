using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangMoveLeft : StateMachineBehaviour
{
    GameObject[] player;
    Movement_Manager _mov_manager;
    // [SerializeField]float move_speed = 1f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _mov_manager = animator.GetComponent<Movement_Manager>();
        // player = GameObject.FindGameObjectsWithTag("Player");
        // foreach (GameObject obj in player){
        //     obj.TryGetComponent<Movement_Manager>(out _mov_manager);
        //     // Debug.Log(obj);
        // }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    //    // Implement code that processes and affects root motion
    // Vector3 delta_pos = animator.deltaPosition;
    // Vector3 rot = animator.transform.rotation.eulerAngles;
    // Quaternion q_delta_rot = animator.deltaRotation;
    // Vector3 delta_rot = q_delta_rot.eulerAngles;
    // delta_rot.y = 0;
    // rot += delta_rot;
    // q_delta_rot = Quaternion.Euler(delta_rot);
    // Quaternion q_rot = Quaternion.Euler(rot);
    // animator.transform.rotation = q_rot;

    Vector3 movement = animator.deltaPosition;
    movement.Scale(Vector3.right+Vector3.up); 
    movement += animator.transform.position;
    animator.transform.position =movement;
    // animator.transform.position += Vector3.right*move_speed*Time.deltaTime;
        // foreach(GameObject p in player){
            // p.transform.position += p.transform.right * move_speed;
        // }
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
