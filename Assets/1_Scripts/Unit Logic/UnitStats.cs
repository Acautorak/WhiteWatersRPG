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

    public float hitchance = 0.8f;
    public float critChance = 0.05f;
    public float dodgeChance = 0.05f;

}
