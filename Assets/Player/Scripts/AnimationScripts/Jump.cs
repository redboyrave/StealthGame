using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : StateMachineBehaviour
{
    GameObject[] player;
    Movement_Manager _mov_manager;
    bool is_crouched;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
         _mov_manager = animator.GetComponent<Movement_Manager>();
        // player = GameObject.FindGameObjectsWithTag("Player");
        // foreach (GameObject obj in player){
            // obj.TryGetComponent<Movement_Manager>(out _mov_manager);
            // Debug.Log(obj);
        // }
        is_crouched= _mov_manager.is_crouched;
        _mov_manager.char_crouch();
        // _mov_manager.is_crouched = false;



        
    }
    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    // override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
 
    // }

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(is_crouched){
            _mov_manager.char_crouch();
        }
        else{
        _mov_manager.char_stand();
        }
        _mov_manager.is_crouched = is_crouched;
    }

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    // override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    
    // }

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    //override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    //override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}
}
