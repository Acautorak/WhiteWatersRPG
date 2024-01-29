using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour
{
    public static BattleSceneManager Instance {get; private set;}

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogWarning("Puko ti je BattleSceneManager");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SetupParty()
    {
        
    }

    
}
