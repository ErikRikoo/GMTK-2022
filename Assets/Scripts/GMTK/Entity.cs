using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public abstract void Play();

    public bool IsDead()
    {
        if (health == 0)
            return true;
        return false;
    }
    private int health;
    private int damage;
}
