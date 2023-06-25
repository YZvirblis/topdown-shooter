using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour
{
    public Upgrade upgrade;
    [SerializeField] Image img;
    [SerializeField] TextMeshProUGUI upgradeName;
    [SerializeField] TextMeshProUGUI upgradeDescription;
    Button btn;

    private void Start()
    {

        btn = GetComponent<Button>();
        btn.onClick.AddListener(Select);
    }

    private void Select()
    {
        upgrade.Apply();
        transform.parent.gameObject.SetActive(false);
        UIManager.Instance.onUpgradeSelectCallback(upgrade);
        Time.timeScale = 1.0f;

    }

    public void Init()
    {
        img.sprite = upgrade.upgradeIcon;
        upgradeName.text = upgrade.upgradeName;
        upgradeDescription.text = upgrade.upgradeDescription;

    }

}
