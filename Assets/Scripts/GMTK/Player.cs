using System.Collections;
using System.Collections.Generic;
using GMTK;
using GMTK.UI.PlayerActions.ActionType;
using Unity.VisualScripting;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private VoidEvent m_PlayerTurn;
    
    [SerializeField] private Player_holder player_holder;
    [SerializeField] private VoidEvent playerIsDead;
    [SerializeField] private VoidEvent playerEscape;

    
    // Start is called before the first frame update
    void Start()
    {
        player_holder.player = this;
    }
    
    public override void Play()
    {
        number_parry = 0;
        m_PlayerTurn.Raise();
    }

    public override void TakeDamage(int damage_taken)
    {
        int real_damage = damage_taken - number_parry;
        if (real_damage < 0)// pas de degat mais le nombre de parades diminue
            number_parry = number_parry - damage_taken;
        else if (real_damage > 0) // il y a des degats et plus de parades 
        {
            number_parry = 0;
            health = health - real_damage;
        }
        else // même nombre de parade que de degat, il y a plus de parade mais pas de degat
            number_parry = 0;

        if (health <= 0)
        {
            health = 0;
            playerIsDead.Raise();
        }
            
    }
    
    public void Attack(Enemies enemies)
    {
        Debug.Log("sante de l'ennemie "+enemies.health);
        Debug.Log("joueur attaque "+enemies.name);
        //Debug.Log(enemies.name);
        enemies.TakeDamage(damage);
        Debug.Log("sante de l'ennemie "+enemies.health);
    }

    public void Parry()
    {
        number_parry++;
    }

    public void Heal(int heal_value)
    {
        health+=heal_value;
        if (health > health_max)
        {
            health = health_max;
        }
    }

    public void escape()
    {
        playerEscape.Raise();
    }
    private int number_parry;
    public int number_action;
    public int score;

    private List<APlayerAction> m_Actions = new();
    public void AddAction(APlayerAction action)
    {
        m_Actions.Add(action);
    }
}
