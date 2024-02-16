using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PartyUnitData
{
    public string unitName;
    public string unitID, partyImageID;
    public int unitLevel, currentHealth;
    public int startingX, startingY;
    public int goldCost, gemsCost;
}
