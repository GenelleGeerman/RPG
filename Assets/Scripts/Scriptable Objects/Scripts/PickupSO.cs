using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pickup", menuName =("Pickup"),order = 2)]
public class PickupSO : ScriptableObject
{
    public int Modifier;
    public PickupType Type;
    public Sprite Sprite;
}

public enum PickupType
{
    Attack,
    Defense,
    Healing,
}