using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRay : MonoBehaviour
{
    public float damage = 100;

    Rigidbody2D rb;
    bool isShooting = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (!isShooting)
        {
            transform.localPosition = new Vector3(0, 0, 0);
        }
    }
    public void Shoot(float speed)
    {
        isShooting = true;
        Vector3 angle = transform.up;
        //angle.z = angle.z + 45;
        gameObject.transform.parent = null;
        rb.velocity = angle * speed;
        StartCoroutine(DestroyGO());
    }

    IEnumerator DestroyGO()
    {
        yield return new WaitForSeconds(8);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isShooting)
        {
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
            if (enemyStats)
            {
                enemyStats.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
