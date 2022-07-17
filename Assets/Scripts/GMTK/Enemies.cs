using System;
using System.Collections;
using System.Collections.Generic;
using GMTK;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class Enemies : Entity
{
    [SerializeField] private Slider m_HealthBar;
    
    [SerializeField] private Player_holder player_holder;


    [SerializeField] private Animator m_Animator;

    [AnimatorParam("m_Animator")]
    [SerializeField] private int m_TakeDamageParam;
    
    
    [AnimatorParam("m_Animator")]
    [SerializeField] private int m_AttackParam;
    
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
        else
        {
            m_Animator.SetTrigger(m_TakeDamageParam);
        }

        
        // TODO: Use a better way to do that
        // Like having setter in entity that has a virtual method OnHealthChanged
        m_HealthBar.value = health;
    }

    private void OnDeath()
    {
        StartCoroutine(c_MovingDownAnim());
    }

    [Header("DeathAnimation")]
    [SerializeField] private AnimationCurve m_AnimationCurve;

    [SerializeField] private float m_Duration;
    
    private IEnumerator c_MovingDownAnim()
    {
        Vector3 originalPosition = transform.position;
        float inverseDUration = 1 / m_Duration;
        float height = -5;

        for (float time = 0; time < m_Duration; time+=Time.deltaTime)
        {
            transform.position = originalPosition +
                new Vector3(0, m_AnimationCurve.Evaluate(time * inverseDUration) * height, 0);
            yield return null;
        }

        transform.position = originalPosition + new Vector3(0, height, 0);

        gameObject.SetActive(false);
    }

    public void attack(Player player)
    {
        player.TakeDamage(damage);
        m_Animator.SetTrigger(m_AttackParam);
    }
}
