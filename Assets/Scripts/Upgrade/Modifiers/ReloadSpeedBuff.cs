using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Modifiers/ReloadBuff" +
    "")]
public class ReloadSpeedBuff : Modifier
{
    public float amount;
    public override void Apply()
    {
        PlayerStats.Instance.reloadTime += amount;
    }
}
