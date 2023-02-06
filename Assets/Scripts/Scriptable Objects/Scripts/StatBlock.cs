using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New StatBlock", menuName = "StatBlock")]
public class StatBlock : ScriptableObject
{
    public int attack;
    public int maxHealth;
    public int speed;
    public int maxEssence;
    public Gradient healthGradient;
}


