using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanClimbCheck : MonoBehaviour
{
    public Movement_Manager _movement_manager;
    public CheckLedge ColliderTop;
    public CheckLedge ColliderBottom;
    [SerializeField]private Transform LeftEdge;
    [SerializeField]private Transform CenterEdge;
    [SerializeField]private Transform RightEdge;
    // public Movement _movement;
    // public WallHang _wallhang;
    [HideInInspector]public bool IsGrabingLedge;
    // Start is called before the first frame update
    void Start()
    {
        BoxCollider top_col =ColliderTop.GetComponent<BoxCollider>();
        float x_pos;
        float y_pos;
        x_pos = top_col.size.x/2;
        y_pos= top_col.size.y/2;
        LeftEdge.position = new Vector3(-x_pos,y_pos,0);
        RightEdge.position = new Vector3(x_pos,y_pos,0);
        CenterEdge.position = new Vector3(0,y_pos,0);        
    }

    void FixedUpdate()
    {   
        check_ledge();       
    }
    void check_ledge(){
        bool grounded = _movement_manager.is_grounded;
        if(!grounded){
            // Debug.Log("Not Grounded");
            foreach (GameObject obj in ColliderBottom.CollidedObjects){
                if(!(ColliderTop.CollidedObjects.Contains(obj))){
                    // Debug.Log("Good to grab");
                    IsGrabingLedge=true;
                    break;
                }
                else{
                    IsGrabingLedge = false;
                }
                
            }
            
        }
        else
        {
            IsGrabingLedge= false;
        }
        if (ColliderBottom.CollidedObjects.Count == 0){
            IsGrabingLedge = false;
        }
    }
}
