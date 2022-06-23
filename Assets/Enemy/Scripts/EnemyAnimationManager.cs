using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationManager : MonoBehaviour
{
    public EnemyStateManager _state;
    public Animator anim;
    //Animator Variables declared here
    float WalkSpeed;
    bool is_patrolling;
    bool is_alert;
    bool is_fighting;
    bool has_gun;
    bool is_dead;
    float AimX;
    float AimY;
    bool is_firing;

    //Animator Variables Hash Path goes here;
    int WalkSpeed_hash;
    int is_patrolling_hash;
    int is_alert_hash;
    int is_fighting_hash;
    int has_gun_hash;
    int is_dead_hash;
    int AimX_hash;
    int AimY_hash;
    int is_firing_hash;


    // Start is called before the first frame update
    void Start()
    {
        WalkSpeed_hash = make_hash("WalkSpeed");
        is_patrolling_hash = make_hash("is_patrolling");
        is_alert_hash = make_hash("is_alert");
        is_fighting_hash = make_hash("is_fighting");
        has_gun_hash = make_hash("has_gun");
        is_dead_hash = make_hash("is_dead");
        AimX_hash = make_hash("AimX");
        AimY_hash = make_hash("AimY");
        is_firing_hash = make_hash("is_firing");

        
    }

    // Update is called once per frame
    void Update()
    {
        // WalkSpeed = _state.WalkSpeed; //Yet to be done;
        is_patrolling = _state.is_patrolling;
        is_alert = _state.is_alert;
        is_fighting = _state.is_fighting;
        has_gun = _state.has_gun;
        // is_dead = _state.is_dead; // Yet to be done;
        // AimX = _state.AimX; //Yet to be done;
        // AimY = _state.AimY; // Yet to be done;
        // is_firing = _state.is_firing; // Yet to be done;

        update_animator();
        
    }

    void update_animator(){
        anim.SetFloat(WalkSpeed_hash,WalkSpeed);
        anim.SetBool(is_patrolling_hash,is_patrolling);
        anim.SetBool(is_alert_hash,is_alert);
        anim.SetBool(is_fighting_hash,is_fighting);
        anim.SetBool(has_gun_hash,has_gun);
        anim.SetBool(is_dead_hash,is_dead);
        anim.SetFloat(AimX_hash,AimX);
        anim.SetFloat(AimY_hash,AimY);
        if(is_firing){
        anim.SetTrigger(is_firing_hash);
        }


    }
    private int make_hash(string str){
        return Animator.StringToHash(str);
    }
}
