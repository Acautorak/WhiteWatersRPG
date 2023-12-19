using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IslandPrefabData", menuName = "ScriptableObjects/IslandPrefabData", order = 1)]
public class IslandPrefabData : ScriptableObject
{
   public Island[] islands;
}
