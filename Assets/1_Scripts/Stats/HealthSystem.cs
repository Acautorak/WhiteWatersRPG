using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// umesto cele papazjanije sa EventHandler<IntEventArgs>, prosto uradi public event Action<int>
public class HealthSystem : MonoBehaviour
{
     public event EventHandler OnDead;
    public event EventHandler<IntEventArgs> OnDamaged;


    [SerializeField] private int health = 100;
    [SerializeField] private int healthMax = 100;


    public void Damage(int damageAmount)
    {
        health -= damageAmount;

        if (health < 0)
        {
            health = 0;
        }

        OnDamaged?.Invoke(this, new IntEventArgs(damageAmount));
        
        if (health == 0)
        {
            Die();
        }

//        Debug.Log(health);
    }

    private void Die()
    {
        OnDead?.Invoke(this, EventArgs.Empty);
    }

    public float GetHealthNormalized()
    {
        return (float)health / healthMax;
    }
}

public class IntEventArgs : EventArgs
{
    public int Value { get; }

    public IntEventArgs(int value)
    {
        Value = value;
    }
}
