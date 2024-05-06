using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// headeri su obicno znak da treba potklase
// takodje monobehaviour bez "zive" logike (start, update, etc) treba da bude scriptableobject
// i onda Unit i Enemy treba samo da imaju referencu na taj SO
public class UnitStats : MonoBehaviour
{

    public UnitType unitType;

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

    [Header("Secondary")]

    public int attackPower;
    public int spellPower;
    public int attackPenetration;
    public int spellPenetration;
    public int accuracy;

    [Space(5)]

    [Header("---%---")]
    public int hitchance = 80;
    public int critChance = 5;
    public int dodgeChance = 5;
    [Space(5)]


    [Header("Misc")]
    public int miscDef;
    public int miscAttack;

    public void CalculateSecondaryStats()
    {
    }
}

[System.Serializable]
public enum UnitType
{
    strength,
    agility,
    inteligence
}
