using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SunSphere : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] GameObject rayPrefab;
    public float damage = 100;


    public int stage = 0;
    public float rotationSpeed = 45f;
    public float shootInterval = 4;
    public float sunRaySpeed = 6;

    [SerializeField] List<Transform> rayPositions = new List<Transform>(8);
    [SerializeField] List<Transform> activeRayPositions = new List<Transform>();

    private void Start()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        List<SunRay> newRays = new List<SunRay>();
        foreach (Transform t in activeRayPositions)
        {
            GameObject newRay = Instantiate(rayPrefab, t.transform);
            newRay.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, t.transform.localRotation.z));
            newRays.Add(newRay.GetComponent<SunRay>());
        }
        yield return new WaitForSeconds(shootInterval);
        foreach (SunRay ray in newRays)
        {
            ray.Shoot(sunRaySpeed);
        }
        yield return new WaitForSeconds(shootInterval / 1.5f);
        StartCoroutine(Shoot());

    }


    private void Update()
    {
        transform.RotateAround(playerTransform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.H))
        {
            AdvanceStage();
        }
    }

    public void AdvanceStage()
    {
        stage++;
        if (stage == 1)
        {
            activeRayPositions.Add(rayPositions[0]);
            activeRayPositions.Add(rayPositions[1]);
        }
        else if (stage == 2)
        {
            activeRayPositions.Add(rayPositions[2]);
            activeRayPositions.Add(rayPositions[3]);
        }
        else if (stage == 3)
        {
            activeRayPositions.Add(rayPositions[4]);
            activeRayPositions.Add(rayPositions[5]);
            activeRayPositions.Add(rayPositions[6]);
            activeRayPositions.Add(rayPositions[7]);
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyStats enemyStats = collision.gameObject.GetComponent<EnemyStats>();
        if (enemyStats)
        {
            enemyStats.TakeDamage(damage);
        }
    }
}
