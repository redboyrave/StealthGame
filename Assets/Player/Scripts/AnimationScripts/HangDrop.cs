using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangDrop : StateMachineBehaviour
{
    GameObject[] player;
    Movement_Manager _mov_manager;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject obj in player){
            obj.TryGetComponent<Movement_Manager>(out _mov_manager);
            Debug.Log(obj);
        }
        _mov_manager.enable_move_coroutine(seconds_to_decimal(0.4f));
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!_mov_manager._movement.enabled){
            _mov_manager.start_movement();
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
    private float seconds_to_decimal(float time){
        return (time * 3f/5f);
    }
  
}
