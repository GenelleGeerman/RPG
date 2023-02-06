using Assets.Scripts.Status;
using System.Collections;
using UnityEngine;

public class SlowStatus : MonoBehaviour, IStatus
{
    public void ApplyStatus(MonoBehaviour behaviour)
    {
        if (behaviour is ISlowable)
        {
            StartCoroutine(DoSlow(behaviour));
        }
    }

    private IEnumerator DoSlow(MonoBehaviour target)
    {
        if (!((ISlowable)target).IsSlowed())
        {
            ((ISlowable)target).Slow(true);
            yield return new WaitForSeconds(3);
            ((ISlowable)target).Slow(false);
        }
    }
}