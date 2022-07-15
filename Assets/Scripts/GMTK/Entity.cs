using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public abstract void Play();
    public abstract void TakeDamage(int damage_taken);
    public bool IsDead()
    {
        if (health == 0)
            return true;
        return false;
    }
    
    protected int health;
    protected int damage;
}
