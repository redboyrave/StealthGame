using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAware : MonoBehaviour
{
    public PlayerInfo p_info;
    void FixedUpdate()
    {
        detection_radius();
    }

    void detection_radius(){
        float distance_to_player = (this.transform.position - p_info.PlayerCurrentCoordinates).magnitude;
        if(distance_to_player<=p_info.NoiseLevel){
            p_info.LastKnowsCoordinates = p_info.PlayerCurrentCoordinates;
            alert();
        }
    } 


    public void alert(){
        Debug.Log("I've been alerted!!");
    }
}
