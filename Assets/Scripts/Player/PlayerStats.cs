using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    #region Singleton
    public static PlayerStats Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        Instance = this;
    }
    #endregion

    [Header("Parameters")]
    public int playerLevel = 1;
    public int health = 3;
    public float currentXp = 0;
    public float targetXp = 100;
    public float moveSpeed = 3f;
    public float baseDamage = 25f;
    public float baseBulletSpeed = 6;
    public int baseMagSize = 3;
    public float baseFireRate = 0.5f;
    public float reloadTime = 1.5f;
    public float baseMagnet = 2;
    public bool sunSphereActive = false;


    [Header("References")]
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI xpText;
    public GameObject sunSphere;

    public delegate void OnHealthChanged();
    public OnHealthChanged onHealthChangedCallback;

    public delegate void OnPlayerLevelUp();
    public OnPlayerLevelUp onPlayerLevelUpCallback;

    SunSphere sunSphereScript;

    private void Start()
    {
        targetXp = (playerLevel + 1) * 10 - 5;
        updateExperienceUI();
        sunSphereScript = sunSphere.GetComponent<SunSphere>();
    }

    #region Damage and death.
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (onHealthChangedCallback != null)
        {
            onHealthChangedCallback.Invoke();
        }
        if (health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("player is dead");
    }
    #endregion

    #region Leveling and XP.
    public void GainXp(int xp)
    {
        currentXp += xp;
        updateExperienceUI();
        if (currentXp >= targetXp)
        {
            LevelUp();
        }
    }
    private void updateExperienceUI()
    {
        xpText.text = "(" + currentXp + "/" + targetXp + ")";
        levelText.text = playerLevel.ToString();
    }
    private void LevelUp()
    {
        playerLevel++;
        if (playerLevel <= 19)
        {
            targetXp += (playerLevel + 1) * 10 - 5;
        }
        else if (playerLevel == 20)
        {
            targetXp += (playerLevel + 1) * 16 - 8;
        }
        else if (playerLevel >= 21 && playerLevel <= 39)
        {
            targetXp += (playerLevel + 1) * 13 - 6;
        }
        else if (playerLevel >= 40 && playerLevel <= 59)
        {
            targetXp += (playerLevel + 1) * 16 - 8;
        }


        if (onPlayerLevelUpCallback != null)
        {
            onPlayerLevelUpCallback.Invoke();
        }
        updateExperienceUI();
    }
    #endregion

    #region Sun Sphere Methods.
    public void SSAddSpeed(float speed)
    {
        sunSphereScript.rotationSpeed += speed;
    }
    public void SSShootInterval(float interval)
    {
        sunSphereScript.shootInterval -= interval;
    }
    public void SSAdvanceStage()
    {
        sunSphereScript.AdvanceStage();
    }
    #endregion
}
