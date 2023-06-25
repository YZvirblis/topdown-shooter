using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int baseDamage;
    public float maxHealth;
    private float currentHealth;
    public float knockbackTime;
    [SerializeField] private GameObject xpPrefab;

    private EnemyMovement movement;


    private void Start()
    {
        currentHealth = maxHealth;
        movement = GetComponent<EnemyMovement>();
    }

    private void FixedUpdate()
    {
        if (currentHealth <= 0)
        {
            GameObject xp = Instantiate(xpPrefab);
            xp.transform.position = transform.position;
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
    public void TakeDamage(float damage)
    {
        Knockback();
        currentHealth -= damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerStats.Instance.TakeDamage(baseDamage);
            Knockback();

        }
    }

    public void Knockback()
    {
        StartCoroutine(movement.Knockback(knockbackTime));
    }

}
