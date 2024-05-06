using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// vazi za ceo projekat: nemoj implementirati stvari koje se ne koriste (npr polja ovde i vrednosti u ConsumableType)
// u najboljem slucaju trosis vreme da prerano dodajes konfuziju i kompleksnost koja nadalje razvoj
// u najgorem slucaju takodje pravis stvari koje se nikad nece korisiti, a i bagove sa njima
// sto manje koristi string za spajanje bilo cega, npr ovde string name, bolje koristi uvek neki enum umesto stringa
[Serializable]
public class Consumable
{
    public ConsumableType consumableType;
    public string name;
    public int count;
    public Sprite icon;
    public string location;
    public int descriptionID;

    public virtual void Consume()
    {

    }
}

public enum ConsumableType
{
    deadMansBlood,
    FirstAidKit,
    healthPotion,
    purifyingSalt,

}
