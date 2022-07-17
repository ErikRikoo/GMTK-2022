using System;
using System.Collections;
using System.Collections.Generic;
using GMTK;
using UnityEngine;
using UnityEngine.UI;

public class Enemies : Entity
{
    [SerializeField] private Slider m_HealthBar;
    
    [SerializeField] private Player_holder player_holder;


    private void Start()
    {
        m_HealthBar.minValue = 0;
        m_HealthBar.maxValue = health_max;
        m_HealthBar.value = health;
    }

    public override void Play()
    {
        attack(player_holder.player);
    }

    public override void TakeDamage(int damage_taken)
    {
        health = health - damage_taken;
        if (health < 0)
        {
            health = 0;
            Debug.Log("Je suis moooooooooooooooort");
            OnDeath();
        }

        
        // TODO: Use a better way to do that
        // Like having setter in entity that has a virtual method OnHealthChanged
        m_HealthBar.value = health;
    }

    private void OnDeath()
    {
        StartCoroutine(c_MovingDownAnim());
    }

    [SerializeField] private float m_MovingDownSpeed = 1;
    
    private IEnumerator c_MovingDownAnim()
    {
        Vector3 originalPosition = transform.position;
        for (float height = 0; height > -5; height -= Time.deltaTime * m_MovingDownSpeed)
        {
            transform.position = originalPosition + new Vector3(0, height, 0);
            yield return null;
        }
        
        gameObject.SetActive(false);
    }

    public void attack(Player player)
    {
        player.TakeDamage(damage);
    }
}
