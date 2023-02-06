using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [SerializeField][Range(0, 10)] private int speed = 5;
    [SerializeField] private Camera cam;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform startPos;

   
    private Vector2 movement;

    private bool isAlive = true;

    private void Start()
    {
        transform.position = startPos.position;
    }

    void Update()
    {
        
    }

    private void OnMove(InputValue value)//Called by controller
    {
        if (isAlive && !GameManager.Instance.IsPaused)
        {
            movement = value.Get<Vector2>();
        }
        else
        {
            movement = new Vector2(0, 0);
        }

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }




}
