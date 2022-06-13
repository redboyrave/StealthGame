using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeNoise : MonoBehaviour

{
    public float noise_duration = .1f;
    public SphereCollider col;

    private bool making_noise= false;
    private float t = 0f;
    private float no_noise = 0.05f;
    // Start is called before the first frame update

    public void Call_Noise(float VolumeRadius){
        making_noise = true;
        make_noise(VolumeRadius);
    }
    private void Update(){
        if (making_noise){
            t += Time.deltaTime;
            if (t >= noise_duration){
                end_noise();
            }
        }
    }
    private void make_noise(float vol_rad){
        Debug.Log("I'm making a noise with radius "+vol_rad+" for "+ noise_duration + " seconds");
        col.radius = vol_rad;
    }
    
    private void end_noise(){
        Debug.Log("I'm done with my noise");
        col.radius = no_noise;
        making_noise = false;
        t=0;
    }
}
