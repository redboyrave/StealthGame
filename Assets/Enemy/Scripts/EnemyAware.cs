using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAware : MonoBehaviour
{
    public PlayerInfo p_info;
    public EnemyStateManager _state;

    void Start(){
        _state = GetComponent<EnemyStateManager>();
    }
    void FixedUpdate()
    {
        noise_detection_radius();
    }

    void noise_detection_radius(){
        float distance_to_player = (this.transform.position - p_info.PlayerCurrentCoordinates).sqrMagnitude;
        if(distance_to_player<=Mathf.Pow(p_info.NoiseLevel,2)){
            p_info.LastKnowsCoordinates = p_info.PlayerCurrentCoordinates;
            alert();
        }
    } 


    public void alert(){
        Debug.Log("I "+this.name+" have been alerted!");
        _state.set_alert();

    }
}
