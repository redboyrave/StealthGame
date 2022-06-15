using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanClimbCheck : MonoBehaviour
{
    public CheckLedge ColliderTop;
    public CheckLedge ColliderBottom;
    public bool IsGrabingLedge;
    private Movement _movement;
    // Start is called before the first frame update
    void Start()
    {
        _movement = GetComponentInParent<Movement>();
        
    }

    void FixedUpdate()
    {   
        if (!_movement.is_grounded){
            Debug.Log(_movement.is_grounded);
            foreach (GameObject obj in ColliderBottom.CollidedObjects){
                if (!ColliderTop.CollidedObjects.Contains(obj)){
                    Debug.Log("I'm good to grab");
                    IsGrabingLedge = true;
                    Debug.Log(_movement.is_hanging);
                    break;
                }
                else{
                    IsGrabingLedge =false;
                    Debug.Log("This is a wall mah duded");
                }
            }

        }
        else{
            IsGrabingLedge = false;
            // Debug.Log("I'm on the ground man, c'mon");
        }
        if (ColliderBottom.CollidedObjects.Count == 0){
            IsGrabingLedge =false;
        }
    }
}
