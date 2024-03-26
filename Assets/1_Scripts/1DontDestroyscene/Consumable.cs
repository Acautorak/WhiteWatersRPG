using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable
{
    public string name;
    public Sprite icon;
    public string location;
    private string description;

    public string GetDescription()
    {
        return description;
    }

    public virtual void Consume()
    {
        
    }
}
