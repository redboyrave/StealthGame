using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    [Header("Main States")]
    [SerializeField]public bool is_patrolling = false;
    [SerializeField]public bool is_alert = false;
    [SerializeField]public bool is_fighting = false;
    [SerializeField]public bool has_gun = false;
    [SerializeField]public GameObject Gun;

    [Header("ObjectReferences")]
    [SerializeField]public EnemyAware _aware;
    [SerializeField]public PatrolRoutes _route;
    [SerializeField]public Renderer _light_indicator;

    [Header("Light Indicator Colors")]
    [SerializeField,ColorUsage(false,true)]private Color patrolling_color;
    [SerializeField,ColorUsage(false,true)]private Color alert_color;
    [SerializeField,ColorUsage(false,true)]private Color fight_color;

    

    private float t= 0f;
    

    void Start()
    {
        set_patrolling();
    }


    void FixedUpdate()
    {
        // test_states(); // Only for testing 


        
    }

    public void set_patrolling(){
        is_patrolling = true;
        is_alert = false;
        is_fighting = false;
        update_light_indicator(patrolling_color);
    }
    public void set_alert(){
        is_alert = true;
        is_patrolling = false;
        is_fighting = false;
        update_light_indicator(alert_color);
    }
    public void set_fighting(){
        is_fighting = true;
        is_patrolling = false;
        is_alert = false;
        update_light_indicator(fight_color);
    }

    void update_light_indicator(Color _color){
        Material[] mats;
        mats = _light_indicator.materials;
        foreach (Material m in mats){
            m.SetColor("_Color",_color);
            m.SetColor("_EmissionColor",_color);
        }
    }

    void test_states(){
        t += Time.deltaTime;
        if (t<=0 || t>0 && t<1){
            set_patrolling();
        }
        if(t>=1 && t<2){
            set_alert();
        }
        if(t>=2 && t<3){
            set_fighting();
        }
        if(t>=3){
            t=0;
        }

    }
}
