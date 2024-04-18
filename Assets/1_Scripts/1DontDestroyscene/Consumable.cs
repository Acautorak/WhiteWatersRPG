using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Consumable
{
    public string name;
    public Sprite icon;
    public string location;
    public int number;
    private string description;

    public string GetDescription()
    {
        return description;
    }

    public virtual void Consume()
    {
        
    }
}
