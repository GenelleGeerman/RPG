using UnityEngine;

public interface IProjectile
{
    int GetVelocity();
    Rigidbody2D GetRigidbody2D();
}