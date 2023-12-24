using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public int gold = 1500;
    public int gems = 50;
    public static PartyManager Instance { get; private set; }
    [SerializeField] private List<PartyUnit> partyUnitList;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Singleton PartyManger: " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        partyUnitList = new List<PartyUnit>(4);
    }

    public void BuyUnitGold(PartyUnit partyUnit)
    {
        if (gold > partyUnit.GetPartyUnitGoldCost())
        {
            Debug.LogWarning("Kupio sam " + partyUnit.name);
            gold -= partyUnit.GetPartyUnitGoldCost();
            partyUnitList.Add(partyUnit);

        }
        else 
        {
            Debug.LogError("Nisam kupio " +  partyUnit.name);
        }
    }

    public void SaveUnitList()
    {
        foreach (PartyUnit partyUnit in partyUnitList)
        {
            partyUnit.SaveUnit();
        }
    }

    public void LoadSavedUnits()
    {
        
    }
}
