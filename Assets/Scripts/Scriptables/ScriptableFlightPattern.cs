using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[CreateAssetMenu(fileName = "FlightPattern", menuName = "ScriptableObjects/SpawnManagerFlightPattern", order = 1)]
public class ScriptableFlightPattern : ScriptableObject
{
    public Animator WhoToMove;
    public PlayableAsset FlighPattern;
  
}