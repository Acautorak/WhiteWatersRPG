using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public  class Consumable
{
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

public interface IConsumable
{
    public void Consume();
}

