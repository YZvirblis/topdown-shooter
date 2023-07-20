using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Modifiers/FireRateBuff" +
    "")]
public class FireRateBuff : Modifier
{
    public float amount;
    public override void Apply()
    {
        PlayerStats.Instance.baseFireRate += amount;
    }
}
