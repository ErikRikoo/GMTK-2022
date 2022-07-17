using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected int health_max;
    
    public abstract void Play();
    public abstract void TakeDamage(int damage_taken);

    protected virtual void Awake()
    {
        health = health_max;
    }

    public bool IsDead()
    {
        if (health == 0)
            return true;
        return false;
    }
    
    protected int health;
    public int damage;
}
