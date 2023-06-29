using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform gunOffset;
    private float lastFireTime;
    private bool fireContinuosley;
    private int bulletsLeft;
    private bool isReloading = false;

    [SerializeField] TextMeshProUGUI bullets;

    private PlayerStats stats;

    private void Start()
    {
        stats = PlayerStats.Instance;
        bulletsLeft = stats.baseMagSize;
    }

    private void FixedUpdate()
    {
        bullets.text = bulletsLeft.ToString();
        if (fireContinuosley)
        {
            float timeSinceLastFire = Time.time - lastFireTime;
            if (timeSinceLastFire >= stats.baseFireRate)
            {
                if (bulletsLeft > 0 && !isReloading)
                {
                    FireBullet();
                    lastFireTime = Time.time;
                }
                else if (bulletsLeft <= 0 && !isReloading)
                {
                    isReloading = true;
                    StartCoroutine(Reload());
                    lastFireTime = Time.time;

                }
            }
        }

        if (bulletsLeft < stats.baseMagSize && !isReloading && Input.GetKeyDown(KeyCode.R))
        {
            isReloading = true;
            StartCoroutine(Reload());
        }
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, gunOffset.position, transform.rotation);
        bullet.GetComponent<Bullet>().damage = stats.baseDamage;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = stats.baseBulletSpeed * gunOffset.transform.right;
        bulletsLeft--;
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(stats.reloadTime);
        bulletsLeft = stats.baseMagSize;
        isReloading = false;

    }

    private void OnFire(InputValue inputValue)
    {
        fireContinuosley = inputValue.isPressed;
    }
}
