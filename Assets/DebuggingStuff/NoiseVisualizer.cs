using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseVisualizer : MonoBehaviour
{
    [SerializeField]private PlayerInfo p_info;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.one * p_info.NoiseLevel;
    }
}
