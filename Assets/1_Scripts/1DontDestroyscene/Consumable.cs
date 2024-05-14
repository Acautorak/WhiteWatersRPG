using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Consumable
{
    public ConsumableType consumableType;
    public int count;
    public string name;
}

public enum ConsumableType
{
    deadMansBlood,
    FirstAidKit,
    healthPotion,
    purifyingSalt,

}
