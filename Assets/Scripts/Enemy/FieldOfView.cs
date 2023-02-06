using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    public GameObject playerRef;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    private Transform player;
    private Vector2 startPos;
    private int speed;
    public bool canSeePlayer;
    [SerializeField] private Vector3 targetPos;
    [SerializeField] private float tileRange;
    private bool returned = true;
    [SerializeField] string colName;
    bool isLookingRight;
    SpriteRenderer sprite;
    float movement;

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        startPos = transform.position;
        targetPos = transform.position;
        sprite = GetComponentInChildren<SpriteRenderer>();
        StartCoroutine(FOVRoutine());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            MoveRandom();
        }
    }

    private void FixedUpdate()
    {
        speed = GetComponent<Enemy>().Speed;
        MoveToPlayer();
    }
    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }
    private void FieldOfViewCheck()
    {
        Collider2D rangeChecks = Physics2D.OverlapCircle(transform.position, radius, targetMask);

        if (rangeChecks != null)
        {
            player = rangeChecks.transform;
            Vector2 directionToTarget = (player.position - transform.position).normalized;
            float distanceToTarget = Vector2.Distance(transform.position, player.position);

            if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
            {
                canSeePlayer = true;
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
    }

    private void MoveToPlayer()
    {
        if (canSeePlayer)
        {
            returned = false;
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            movement = transform.position.x - player.position.x;
        }
        else
        {
            if (Vector2.Distance(transform.position, startPos) > 0.1 && !returned) //Moving back to start position
            {
                transform.position = Vector2.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
                if (Vector2.Distance(transform.position, startPos) < 1)
                {
                    returned = true;
                }
            }
            else
            {

                MoveRandom();
            }
        }
        FlipSprite();

    }
    private void MoveRandom()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPos) <= 0.5)
        {
            do
            {
                targetPos = startPos + new Vector2(Random.Range(-tileRange, tileRange), Random.Range(-tileRange, tileRange));
                movement = transform.position.x - targetPos.x;
            } while (CheckForWall());
        }
    }

    private bool CheckForWall()
    {

        if (Physics2D.Raycast(targetPos, -transform.up, tileRange, obstructionMask))
        {
            return true;
        }
        return false;
    }

    private void FlipSprite()
    {
        if ((movement > 0 && !isLookingRight) || (movement < 0 && isLookingRight))
        {
            isLookingRight = !isLookingRight;
            sprite.flipX = !sprite.flipX;
        }
    }
}
