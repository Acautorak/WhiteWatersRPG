using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyUnit : MonoBehaviour
{
    

    public Sprite unitSprite;
    [SerializeField] private GameObject unitPrefab;
    [SerializeField] private GridPosition startingGridPosition;
    [SerializeField] PartyUnitData data;

    public string GetPartyUnitName()
    {
        return data.unitName;
    }
    public int GetPartyUnitGoldCost()
    {
        return data.goldCost;
    }

    public void SaveUnit()
    {
        string json = JsonUtility.ToJson(this);
        PlayerPrefs.SetString(data.unitName, json);
    }

    public PartyUnit LoadUnit(string partyUnitName)
    {
        if (PlayerPrefs.HasKey(partyUnitName))
        {
            string json = PlayerPrefs.GetString(partyUnitName);
            PartyUnit data = JsonUtility.FromJson<PartyUnit>(json);

            return data;
        }
        else
        {
            Debug.LogError("Loadovo sam kurac!");
            return null;
        }
    }
}


[Serializable]
    public class PartyUnitData
    {
        public int goldCost;
        public int gemsCost;
        public string unitName;
        public string unitDescription;
        public int level;
        public PartyUnitData(int goldCost, int gemsCost, string unitName, string unitDescription, int level)
        {
            this.goldCost = goldCost;
            this.gemsCost = gemsCost;
            this.unitName = unitName;
            this.unitDescription = unitDescription;
            this.level = level;
        }
    }