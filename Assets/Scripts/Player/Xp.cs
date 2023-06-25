using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xp : MonoBehaviour
{
    [SerializeField] int xp;


    private bool isPlayerInRange;
    private Vector2 DirectionToPlayer;
    private float playerAwarenessDistance;
    private Transform player;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    private Rigidbody2D rb;
    private Vector2 targetDir;

    PlayerStats stats;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject.transform;
        rb = GetComponent<Rigidbody2D>();
        stats = PlayerStats.Instance;
        playerAwarenessDistance = stats.baseMagnet;
    }

    private void Update()
    {
        Vector2 xpToPlayerVector = player.position - transform.position;
        DirectionToPlayer = xpToPlayerVector.normalized;
        if (xpToPlayerVector.magnitude <= playerAwarenessDistance)
        {
            isPlayerInRange = true;
        }
        else { isPlayerInRange = false; }
    }

    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }

    private void UpdateTargetDirection()
    {
        if (isPlayerInRange)
        {
            targetDir = DirectionToPlayer;
        }
    }
    private void RotateTowardsTarget()
    {
        if (targetDir == Vector2.zero)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, targetDir);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        rb.SetRotation(rotation);
    }
    private void SetVelocity()
    {
        if (targetDir == Vector2.zero)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.velocity = transform.up * moveSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            stats.GainXp(xp);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, playerAwarenessDistance);
    }
}
