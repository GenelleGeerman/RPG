using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerBasicAttack : MonoBehaviour, IProjectile
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int damage;
    [SerializeField] private int velocity;
    [SerializeField] private Transform target;
    public int Damage { get => damage; set => damage = value; }
    public Status Status { get; set; }
    public IStatus StatusEffect { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;
        Destroy(gameObject, 10f);
        rb.AddForce(transform.right * velocity, ForceMode2D.Impulse);

    }
    private void FixedUpdate()
    {
        if (StatusEffect is IAugment)
        {
            StatusEffect.ApplyStatus(this);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IgnoredTags(collision)) return;

        if (collision.TryGetComponent(out Enemy enemy))
        {

            enemy.TakeDamage(damage);
            if (HasStatus())
            {
                StatusEffect.ApplyStatus(enemy);
            }
        }
        Destroy(gameObject);
    }

    private bool IgnoredTags(Collider2D collision)
    {
        return collision.tag == "Essence"
                    || collision.tag == "Pickup"
                    || collision.tag == "BasicAttack"
                    || collision.tag == "SpecialAttack";
    }


    public bool HasStatus()
    {
        return Status != Status.None;
    }

    public int GetVelocity()
    {
        return velocity;
    }

    public Rigidbody2D GetRigidbody2D()
    {
        return rb;
    }
}
