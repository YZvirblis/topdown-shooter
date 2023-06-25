using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
        if (enemyStats)
        {
            enemyStats.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
