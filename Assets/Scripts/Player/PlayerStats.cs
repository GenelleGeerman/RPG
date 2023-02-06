using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour, ITakeDamage, IDefend
{
    [SerializeField] private int health;
    [SerializeField, Tooltip("StatBlock that the player uses")]
    private StatBlock statBlock;

    [SerializeField] private int maxHealth;

    [SerializeField] private int attack;
    [SerializeField] private float invulnerableTime;
    [SerializeField] private bool invulnerable;
    public UnityEvent OnDeath;
    private bool neverDie = false;
    private bool isDefending = false;
    public StatBlock StatBlock { get => statBlock; }

    public int MaxHealth { get => maxHealth; }

    public int Attack { get => attack; set => attack = value; }
    public int Health { get => health; set => health = value; }

    void Start()
    {
        Health = statBlock.maxHealth;
        maxHealth = statBlock.maxHealth;
        attack = statBlock.attack;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IAttackable attackable))
        {
            TakeDamage(attackable.GetAttack());
        }
    }


    public void TakeDamage(int damage)
    {
        if (ShouldNotTakeDamage()) return;

        health = Math.Max(0, health - damage);
        if (IsAlive())
        {
            StartCoroutine(BecomeInvulnerable());
        }
        else
        {
            health = 0;
            OnDeath?.Invoke();
        }
    }
    public void Heal(int heal)
    {
        health += Math.Min(maxHealth, health + heal);
    }
    
    public void ToggleNeverDie()
    {
        neverDie = !neverDie;
    }
    public bool IsDefending()
    {
        if (isDefending)
        {
            isDefending = false;
            return true;
        }
        return false;
    }

    private IEnumerator BecomeInvulnerable()
    {
        invulnerable = true;
        yield return new WaitForSeconds(invulnerableTime);
        invulnerable = false;
    }
    private bool IsAlive()
    {
        return health > 0;
    }
    private bool ShouldNotTakeDamage()
    {
        if (neverDie || invulnerable) return true;
        if (IsDefending()) return true;
        return false;
    }
}