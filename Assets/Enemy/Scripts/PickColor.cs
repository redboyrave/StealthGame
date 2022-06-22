using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickColor : MonoBehaviour
{
    [SerializeField] private Renderer mesh_renderer;
    // Start is called before the first frame update
    void Start()
    {
        mesh_renderer = GetComponentInChildren<Renderer>();
        hue_shift();
    }

    


    void hue_shift(){
        Material[] mats = mesh_renderer.materials;
        int range = mats.Length;
        //Checks all material slots in range (Range is the lenght of the list of materials).
        for (int mat = 0; mat<range ; mat++){
            //Checks if the property exist before checking it's value, if
            //the property doesn't exist in the shader, we ignore that material
            if (!mats[mat].HasProperty("Texture2D_248f24fdf2874f4d9b293b32f9f056d7")){
                continue;
            }
        //Sets the color of the Shader value to the value passed to the function
        mats[mat].SetFloat("Vector1_52e2ce3741cd4315af7650c023c22596",Random.Range(0f,1f));
        mats[mat].SetFloat("Vector1_9954426a34b0417ea31c8467a9aab37f",Random.Range(-0.41f,0.08f));
        }
    }
}
