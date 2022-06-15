using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLedge : MonoBehaviour
{
    public List<GameObject> CollidedObjects = new List<GameObject>();

    void OnTriggerEnter(Collider other){
        if (!other.gameObject.CompareTag("Enemy")){
            if (!CollidedObjects.Contains(other.gameObject)){
                CollidedObjects.Add(other.gameObject);

            }
        }
    }
    
    void OnTriggerExit(Collider other){
        if (CollidedObjects.Contains(other.gameObject)){
            CollidedObjects.Remove(other.gameObject);
        }

    }
}
