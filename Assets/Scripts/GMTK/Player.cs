using System.Collections;
using System.Collections.Generic;
using GMTK;
using GMTK.UI;
using GMTK.UI.PlayerActions.ActionType;
using Unity.VisualScripting;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private BetLauncher m_BetLauncher;
    
    [Header("Events")]
    [SerializeField] private VoidEvent m_EndOfTurn;
    
    [SerializeField] private VoidEvent m_PlayerTurn;
    [SerializeField] private VoidEvent m_ExecuteActions;
    [SerializeField] private VoidEvent playerIsDead;
    [SerializeField] private VoidEvent playerEscape;

    [SerializeField] private IntEvent m_DamageChanged;
    [SerializeField] private IntEvent m_PAChanged;
    [SerializeField] private IntPairEvent m_HealthChanged;
    
    [Header("Variables")]
    [SerializeField] private Player_holder player_holder;

    
    
    // Start is called before the first frame update
    void Start()
    {
        player_holder.player = this;
        m_PAChanged.Raise(m_number_action);
        m_DamageChanged.Raise(damage);
        m_HealthChanged.Raise( new IntPair()
        {
            Item1 = Health,
            Item2 = health_max
        });
        m_ExecuteActions.Register(OnExecuteActions);

        StartCoroutine(c_Test());
    }

    private IEnumerator c_Test()
    {
        yield return new WaitForSeconds(1);
        Play();
        // TakeDamage(2);
        // yield return new WaitForSeconds(1);
        // TakeDamage(1);


    }

    public override void Play()
    {
        number_parry = 0;
        m_PlayerTurn.Raise();
    }

    private IEnumerator ActionsExecutor;
    
    private void OnExecuteActions()
    {
        ActionsExecutor = ExecuteActions();
        ActionsExecutor.MoveNext();
    }

    private IEnumerator ExecuteActions()
    {
        int i = 0;
        foreach (var action in m_Actions)
        {
            m_BetLauncher.LaunchBet((face) =>
            {            
                // TODO: Display and say which action has been executed for player and UI
                Debug.Log($"{action.GetType().Name} - {action.BetType.GetType().Name} - {action.BetType.DiceFace}");
                action.ExecuteIfPossible(face, this);
                ActionsExecutor.MoveNext();
            });
            yield return null;
            ++i;
        }
        m_Actions.Clear();
        m_EndOfTurn.Raise();
    }

    public override void TakeDamage(int damage_taken)
    {
        int real_damage = damage_taken - number_parry;
        if (real_damage < 0)// pas de degat mais le nombre de parades diminue
            number_parry = number_parry - damage_taken;
        else if (real_damage > 0) // il y a des degats et plus de parades 
        {
            number_parry = 0;
            Health = Health - real_damage;
        }
        else // même nombre de parade que de degat, il y a plus de parade mais pas de degat
            number_parry = 0;

        if (Health <= 0)
        {
            Health = 0;
            playerIsDead.Raise();
        }
            
    }
    
    public void Attack(Enemies enemies)
    {
        //Debug.Log("sante de l'ennemie "+enemies.health);
        //Debug.Log("joueur attaque "+enemies.name);
        //Debug.Log(enemies.name);
        enemies.TakeDamage(damage);
        //Debug.Log("sante de l'ennemie "+enemies.health);
    }

    public void Parry()
    {
        number_parry++;
    }

    public void Heal(int heal_value)
    {
        Health+=heal_value;
        if (Health > health_max)
        {
            Health = health_max;
        }
    }

    public void escape()
    {
        playerEscape.Raise();
    }
    private int number_parry;

    public int number_action
    {
        get => m_number_action;
        set
        {
            m_number_action = value;
            m_PAChanged.Raise(value);
        }
    }

    public int Damage
    {
        get => damage;
        set
        {
            damage = value;
            m_DamageChanged.Raise(value);
        }
    }

    public int Health
    {
        get => health;
        set
        {
            health = value;
            m_HealthChanged.Raise(new IntPair()
            {
                Item1 = value,
                Item2 = health_max
            });
        }
    }
    
    
    public int m_number_action;
    public int score;

    private List<APlayerAction> m_Actions = new();
    public void AddAction(APlayerAction action)
    {
        m_Actions.Add(action);
    }
}
