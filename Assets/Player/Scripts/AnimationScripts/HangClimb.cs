using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangClimb : StateMachineBehaviour
{
    GameObject[] player;
    Movement_Manager _mov_manager;
    public float xz_plane_offset = 1f;
    public float y_offset = 1f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _mov_manager = animator.GetComponent<Movement_Manager>();
        Debug.Log(_mov_manager);
        // player = GameObject.FindGameObjectsWithTag("Player");
        // foreach (GameObject obj in player){
        //     obj.TryGetComponent<Movement_Manager>(out _mov_manager);
        //     Debug.Log(obj);
        // }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _mov_manager.enable_move_coroutine(.1f);
    }
    // OnStateIK is called right after Animator.OnAnimatorIK()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    //    Implement code that sets up animation IK (inverse kinematics)
        Vector3 offset = animator.transform.position;
        offset.x += animator.deltaPosition.x * xz_plane_offset;
        offset.z += animator.deltaPosition.z * xz_plane_offset;
        offset.y += animator.deltaPosition.y * y_offset;
        animator.transform.position = offset;
    }

}
