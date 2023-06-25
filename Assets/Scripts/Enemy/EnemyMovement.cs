using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float knockbackRate;

    private Rigidbody2D rb;
    private Vector2 targetDirection;
    private Transform playerTransform;
    private bool isAggro = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if (isAggro)
        {
            UpdateTargetDirection();
            RotateTowardsTarget();
            SetVelocity();
        }
    }

    private void UpdateTargetDirection()
    {
        Vector2 enemyToPlayerVector = playerTransform.position - transform.position;
        targetDirection = enemyToPlayerVector.normalized;
    }
    private void RotateTowardsTarget()
    {
        if (targetDirection == Vector2.zero)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        rb.SetRotation(rotation);
    }
    private void SetVelocity()
    {
        if (targetDirection == Vector2.zero)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.velocity = transform.up * moveSpeed;
        }
    }

    public IEnumerator Knockback(float knockbackTime)
    {
        isAggro = false;
        rb.velocity = -targetDirection * knockbackRate;
        yield return new WaitForSeconds(knockbackTime);
        isAggro = true;

    }


}
