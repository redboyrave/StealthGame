using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerInfo : ScriptableObject
{
    public Vector3 PlayerCurrentCoordinates;
    public Vector3 LastKnowsCoordinates;
    public float NoiseLevel;
    public float Health;
    public int Score;

}
