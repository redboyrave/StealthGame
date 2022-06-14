using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeNoise : MonoBehaviour

{
    public float noise_duration = .1f;
    public PlayerInfo p_info;

    private bool making_noise= false;
    private float t = 0f;
    private float no_noise = 0f;
    // Start is called before the first frame update

    private void Start(){
        end_noise(); //Sets noise to minimum by default.
    }
    public void Call_Noise(float VolumeRadius){ //Method to be used when you can to make a sound
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
        t=0f; //Resets time everytime de method is called, so the noise will continue has long has the method is called.
        if (!(p_info.NoiseLevel == vol_rad)){
            p_info.NoiseLevel = vol_rad;
        }
    }
    
    private void end_noise(){
        // Debug.Log("I'm done with my noise");
        p_info.NoiseLevel = no_noise;
        making_noise = false;
        t=0;
    }
}
