using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TattooGlow : MonoBehaviour
{
    public Renderer player_renderer;
    [ColorUsage(false,true)]
    public Color Glow_color;
    // Start is called before the first frame update
    public float change_speed = 5f;
    private float ammount = 0.0f;
    void Update()
    {
        ammount = Mathf.PingPong(Time.time*change_speed/10,1);
        Color current_color = Color.Lerp(Color.black,Glow_color,ammount);
        change_mat_glow(current_color);

    }

    void change_mat_glow(Color c){
        Material[] mats = player_renderer.sharedMaterials;
        int range = player_renderer.materials.Length;
        //Checks all material slots in range (Range is the lenght of the list of materials).
        for (int mat = 0; mat<range ; mat++){
            //Checks if the property exist before checking it's value, if
            //the property doesn't exist in the shader, we ignore that material
            if (!mats[mat].HasProperty("Boolean_fdff3ea8c913415e89eb01af70483d56")){
                continue;
            }
            //Checks Shader Material "Use Emission" value
            if (mats[mat].GetInt("Boolean_fdff3ea8c913415e89eb01af70483d56")==1){
                //Sets the color of the Shader value to the value passed to the function
                mats[mat].SetColor("Color_2ed096ea28534f8dbd4ba4f43c153090",c);
            }
            }
        }
 }
