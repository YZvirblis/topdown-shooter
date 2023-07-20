using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Modifiers/Upgrade" +
    "")]
public class Upgrade : ScriptableObject
{
    public string upgradeName;
    [TextArea] public string upgradeDescription;
    public Sprite upgradeIcon;
    public List<Modifier> modifiers;
    public List<Upgrade> childUpgrades;

    public virtual void Apply()
    {
        foreach (Modifier mod in modifiers)
        {
            mod.Apply();
        }
        AddChildUpgrades();
    }

    public void AddChildUpgrades()
    {
        if (childUpgrades != null)
        {
            UIManager uiManager = UIManager.Instance;
            foreach (Upgrade upgrade in childUpgrades)
            {
                uiManager.upgrades.Add(upgrade);
            }
        }
    }

}
