using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Consumable
{
    public ConsumableType consumableType;
    public string name;
    public int count;
    public Sprite icon;
    public string location;
    public int descriptionID;

    public virtual void Consume()
    {

    }
}

public enum ConsumableType
{
    deadMansBlood,
    FirstAidKit,
    healthPotion,
    purifyingSalt,

}
