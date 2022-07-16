using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] private int health_max;
    
    public abstract void Play();
    public abstract void TakeDamage(int damage_taken);

    void Awake()
    {
        health = health_max;
    }

    public bool IsDead()
    {
        if (health == 0)
            return true;
        Debug.Log(this.name + "is alive with" + this.health);
        return false;
    }
    
    public int health;//remettre en protected aprés le test
    public int damage;
}
