using System.Collections;
using System.Collections.Generic;
using GMTK;
using UnityEngine;

public class Enemies : Entity
{
    [SerializeField] private Player_holder player_holder;
    
    public override void Play()
    {
        attack(player_holder.player);
    }

    public override void TakeDamage(int damage_taken)
    {
        health = health - damage_taken;
        if (health < 0)
            health = 0;
    }

    public void attack(Player player)
    {
        player.TakeDamage(damage);
    }
}
