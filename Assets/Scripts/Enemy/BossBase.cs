using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBase : Enemy
{
    private void Update()
    {
        HealthCheck();
        DeathFlag();
    }

    private void DeathFlag()
    {
        if (GetHealth() <= 0)
        {
            GameManager.Instance.BossDefeated();
        }
    }
}