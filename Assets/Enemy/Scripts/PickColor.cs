using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickColor : MonoBehaviour
{
    [SerializeField] private Renderer[] mesh_renderer;
    // Start is called before the first frame update
    void Start()
    {
        mesh_renderer = GetComponentsInChildren<Renderer>();
        pick_color();
    }

    


    void pick_color(){
        foreach (Renderer renderer in mesh_renderer){
            Material[] mats = renderer.materials;
            foreach(Material mat in mats){
            //Checks if the property exist before checking it's value, if
            //the property doesn't exist in the shader, we ignore that material
                if (!mat.HasProperty("Texture2D_248f24fdf2874f4d9b293b32f9f056d7")){
                    continue;
                }
            //Sets the color of the Shader value to the value passed to the function
            mat.SetFloat("Vector1_52e2ce3741cd4315af7650c023c22596",Random.Range(0f,1f));
            }
        }
    }
}
