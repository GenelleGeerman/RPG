using UnityEngine;
using UnityEngine.EventSystems;

public class SeekingStatus : MonoBehaviour, IStatus, IAugment
{

    public void ApplyStatus(MonoBehaviour behaviour)
    {
        if (!(behaviour is IProjectile)) return;

        Transform target = FindClosestEnemy(behaviour.transform);

        if (target != null)
        {
            Rigidbody2D rb = ((IProjectile)behaviour).GetRigidbody2D();
            Vector2 direction = GetDirection(rb.position, target.transform.position);
            SetVelocity(behaviour, rb, direction);
        }

    }

    private void SetVelocity(MonoBehaviour behaviour, Rigidbody2D rb, Vector2 direction)
    {
        rb.angularVelocity = Aim(direction, behaviour);
        rb.velocity = behaviour.transform.right * ((IProjectile)behaviour).GetVelocity();
    }

    private Vector2 GetDirection(Vector2 position, Vector2 targetPosition)
    {
        Vector2 direction = targetPosition - position;
        direction.Normalize();
        return direction;

    }
    private float Aim(Vector2 direction, MonoBehaviour behaviour)
    {
        float rotateAmount = Vector3.Cross(direction, behaviour.transform.right).z;
        return -rotateAmount * 400f;
    }
    private Transform FindClosestEnemy(Transform transform)
    {
        GameObject[] enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemyArray == null) return null;

        Transform closest = null;
        float prevDistance = Mathf.Infinity;

        foreach (GameObject gameObject in enemyArray)
        {
            Vector3 diff = gameObject.transform.position - transform.position;
            float distance = diff.sqrMagnitude;

            if (distance < prevDistance)
            {
                closest = gameObject.transform;
                prevDistance = distance;
            }
        }
        return closest;
    }
}
