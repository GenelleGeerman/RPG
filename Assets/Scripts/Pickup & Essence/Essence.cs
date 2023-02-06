using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Essence : MonoBehaviour
{
    [SerializeField] private EssenceSO essenceSO;
    [SerializeField] private Sprite sprite;
    [Header("Do not Edit")]
    [SerializeField] private EssenceType essenceType;
    [SerializeField] private int essenceAmount;
    [SerializeField] private Status status;

    void Start()
    {
        essenceType = essenceSO.essenceType;
        essenceAmount = essenceSO.essenceBarAmount;
        status = essenceSO.buffType;
        sprite = essenceSO.sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<EssenceManager>().AddEssence(essenceAmount, status);
            Destroy(gameObject);
        }
    }
}
