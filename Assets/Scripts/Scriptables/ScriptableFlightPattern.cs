using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[CreateAssetMenu(fileName = "FlightPattern", menuName = "ScriptableObjects/SpawnManagerFlightPattern", order = 1)]
public class ScriptableFlightPattern : ScriptableObject
{
    public Transform WhoToMove;
    public PlayableAsset FlighPattern;
  
}