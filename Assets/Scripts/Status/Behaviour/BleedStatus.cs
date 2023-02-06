using UnityEngine;
using System.Collections;

public class BleedStatus :MonoBehaviour, IStatus
{
    [SerializeField] private int bleedTime = 5;
    public void ApplyStatus(MonoBehaviour behaviour)
    {
        if (behaviour is ITakeDamage)
        {
           StartCoroutine( DoTick((ITakeDamage)behaviour));
        }
    }

    public IEnumerator DoTick(ITakeDamage target)
    {
        for (int i = 0; i < bleedTime; i++)
        {
            yield return new WaitForSeconds(1);
            target.TakeDamage(1);
        }
    }
}