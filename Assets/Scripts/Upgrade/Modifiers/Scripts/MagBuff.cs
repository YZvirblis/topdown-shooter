using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Modifiers/MagBuff" +
    "")]
public class MagBuff : Modifier
{
    public int amount;
    public override void Apply()
    {
        PlayerStats.Instance.baseMagSize += amount;
    }
}
