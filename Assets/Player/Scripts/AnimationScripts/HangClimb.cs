using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangClimb : StateMachineBehaviour
{
    GameObject[] player;
    Movement_Manager _mov_manager;
    public float xz_plane_offset = 1f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject obj in player){
            obj.TryGetComponent<Movement_Manager>(out _mov_manager);
            Debug.Log(obj);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _mov_manager.enable_move_coroutine(.1f);
    }


}
