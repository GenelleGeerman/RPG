using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Units/Enemy")]
public class EnemySO : ScriptableObject
{
    public int maxHp;
    public int currentHp;
    public float speed;
    public Sprite sprite;
    
    public void PrintHP()
    {
        Debug.Log($"HP: {currentHp}/{maxHp}");
    }
    
}
