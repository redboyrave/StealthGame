using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(menuName = "Patrol Route")]
[ExecuteInEditMode]
public class PatrolRoutes : MonoBehaviour
{
    [Header("Patrol Checkpoints")]
    [SerializeField]public Transform[] checkpoints;

    [HideInInspector]public int current_checkpoint;
    // Start is called before the first frame update
    // void Start()
    // {
    //     make_path();
    // }

    // // Update is called once per frame
    void Update()
    {
        make_path();
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.white;
        for(int i = 0; i<=checkpoints.Length; i++ ){
            Gizmos.DrawSphere(checkpoints[i].position,.05f);
            Gizmos.DrawLine(checkpoints[i].position,checkpoints[next_checkpoint(i)].position);
        }
    }


    void make_path(){
        int childCount = this.gameObject.transform.childCount;
        checkpoints = new Transform[childCount];
        for (int i = 0; i <= childCount; i++){
            checkpoints[i] = this.gameObject.transform.GetChild(i);
        }
    }
    public int next_checkpoint(int point){
        int next_point = point +1;
         if(next_point>=checkpoints.Length){
            next_point=0;
         }
        return next_point;
    }

}
