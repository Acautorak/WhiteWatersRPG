using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public int gold = 1500;
    public int gems = 50;
    public static PartyManager Instance { get; private set; }
    [SerializeField] private List<PartyUnit> partyUnitList = new List<PartyUnit>();

    public  event EventHandler OnGoldChanged;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Singleton PartyManger: " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void BuyUnitGold(PartyUnit partyUnit)
    {
        if (gold > partyUnit.goldCost)
        {
            Debug.LogWarning("Kupio sam " + partyUnit.unitName);
            gold -= partyUnit.goldCost;
            partyUnitList.Add(partyUnit);
            OnGoldChanged?.Invoke(this, EventArgs.Empty);

        }
        else 
        {
            Debug.LogError("Nisam kupio " +  partyUnit.unitName);
        }
    }

    public void SaveUnitList()
    {
        
    }

    public void LoadSavedUnits()
    {
        
    }
}
