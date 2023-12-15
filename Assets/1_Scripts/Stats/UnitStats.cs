using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    [Header("Defence")]
    public int armor;
    public int spellArmor;
    public int elementalResistance;
    [Space(5)]

    [Header("Core stats")]
    public int strength;
    public int inteligence;
    public int agility;
    public int perception;
    public int initiative;
    [Space(5)]

    [Header("---%---")]
    public int hitchance = 80;
    public int critChance = 5;
    public int dodgeChance = 5;
    [Space(5)]

    [Header("Misc")]
    public int miscDef;
    public int miscAttack;


}
