using System;


[Serializable]
public  class Consumable
{
    public int count;
    public string name;
}

public enum ConsumableType
{
    deadMansBlood,
    FirstAidKit,
    healthPotion,
    purifyingSalt,

}

public interface IConsumable
{
    public void Consume();
}

