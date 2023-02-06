using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Essence", menuName = "Essence", order = 3)]
public class EssenceSO : ScriptableObject
{
    [SerializeField] public EssenceType essenceType;
    [SerializeField] public int essenceBarAmount;
    [SerializeField] public Status buffType;
    [SerializeField] public Sprite sprite;
}
public enum EssenceType
{
    Goblin,
    Turtle,
    Slime,
    Bat
}