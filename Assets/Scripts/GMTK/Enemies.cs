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

    public void attack(Player player)
    {
        
    }
}
