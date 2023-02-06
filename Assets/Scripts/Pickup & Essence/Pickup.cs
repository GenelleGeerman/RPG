using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private PickupSO pickup;
    [SerializeField] private int modifier;
    [SerializeField] private PickupType type;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        modifier = pickup.Modifier;
        type = pickup.Type;
        spriteRenderer.sprite = pickup.Sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (type == PickupType.Healing)
            {
                HealthCheck(collision.gameObject);
            }

        }
    }

    private void HealthCheck(GameObject player)
    {
        PlayerStats stats =  player.GetComponent<PlayerStats>();
        if (stats.Health < stats.MaxHealth)
        {
            stats.Heal(modifier);
            Destroy(gameObject);
        }
    }
}
