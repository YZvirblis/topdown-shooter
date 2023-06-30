using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.WSA;

public class UIManager : MonoBehaviour
{
    private PlayerStats playerStats;
    [SerializeField] GameObject heartUIPrefab;
    [SerializeField] Transform healthContainer;
    public List<Upgrade> upgrades;
    [SerializeField] GameObject upgradesContainer;
    [SerializeField] GameObject upgradeCardPrefab;
    public bool isMenuOpen;

    public delegate void OnUpgradeSelect(Upgrade upgrade);
    public OnUpgradeSelect onUpgradeSelectCallback;

    #region Singleton
    public static UIManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        Instance = this;
    }
    #endregion

    private void Start()
    {
        upgradesContainer.SetActive(false);
        playerStats = PlayerStats.Instance;
        playerStats.onHealthChangedCallback += UpdateHealthUI;
        playerStats.onPlayerLevelUpCallback += LevelUp;
        onUpgradeSelectCallback += RemoveUpgradeFromList;
        UpdateHealthUI();
    }
    private void UpdateHealthUI()
    {
        foreach (Transform child in healthContainer)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < playerStats.health; i++)
        {
            GameObject heart = Instantiate(heartUIPrefab);
            heart.transform.SetParent(healthContainer, false);
        }
    }

    private void Update()
    {
        if (isMenuOpen)
        {
            UnityEngine.Cursor.visible = true;
        }
        else
        {
            UnityEngine.Cursor.visible = false;
        }
    }

    private void LevelUp()
    {
        foreach (Transform child in upgradesContainer.transform)
        {
            Destroy(child.gameObject);
        }
        if (upgrades.Count > 0)
        {
            upgradesContainer.SetActive(true);
            isMenuOpen = true;
            Time.timeScale = 0f;
            for (int i = 0; i < 4; i++)
            {
                GameObject g = Instantiate(upgradeCardPrefab);
                g.transform.SetParent(upgradesContainer.transform, false);
                UpgradeCard u = g.GetComponent<UpgradeCard>();
                u.upgrade = upgrades[Random.Range(0, upgrades.Count)];
                u.Init();
            }
        }
        else
        {
            Debug.Log("NO UPGRADES LEFT");
        }

    }

    private void RemoveUpgradeFromList(Upgrade upgrade)
    {
        upgrades.Remove(upgrade);
    }
}
