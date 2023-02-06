using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EssenceManager : MonoBehaviour
{
    [SerializeField] private float essence;
    [SerializeField] private int maxEssence;
    public float Essence { get => essence; set => essence = value; }
    public int MaxEssence { get => maxEssence; }
    public UnityEvent<Status> OnMaxEssence;
    void Start()
    {
        StatBlock statBlock = GetComponent<PlayerStats>().StatBlock;
        Essence = 0;
        maxEssence = statBlock.maxEssence;
    }
    public void AddEssence(int amount, Status type)
    {
        if (essence < maxEssence)
        {
            essence = Math.Min(maxEssence, essence + amount);
        }
        else if(essence >= maxEssence)
        {
            OnMaxEssence?.Invoke(type);
        }
    }
}
