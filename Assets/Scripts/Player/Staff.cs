
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
public class Staff : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    [SerializeField] private float offset = 0.45f;
    public float Offset { get => offset; set => offset = value; }
    private float mouseX;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        mouseX = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).x;
    }
    void FixedUpdate()
    {

        float distance = GetDistance();
        if (distance > 0.5)
        {
            SetRight();
        }
        else if (distance < -0.5)
        {
            SetLeft();
        }
        else
        {
            //do nothing
        }

    }
    private float GetDistance()
    {
        return mouseX - transform.position.x;
    }

    private void SetLeft()
    {
        transform.localPosition = new Vector2(-offset, 0);
        spriteRenderer.flipX = true;
    }

    private void SetRight()
    {
        transform.localPosition = new Vector2(offset, 0);
        spriteRenderer.flipX = false;
    }

    public Vector3 GetSpawnPosition()
    {
        return transform.position;
    }
}