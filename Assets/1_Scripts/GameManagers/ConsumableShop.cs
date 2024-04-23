using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ConsumableShop : MonoBehaviour
{

    public static ConsumableShop Instance { get; private set; }
    [SerializeField]
    public Consumable slot1, slot2, slot3, slot4;
    [SerializeField]
    private List<Consumable> allConsumables;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            Debug.Log("Puko je Consumable Shop singleton");
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public List<Consumable> GetAllConsumableList()
    {
        return allConsumables;
    }


}
