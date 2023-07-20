using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Modifiers/SS Fire Rate Buff" +
    "")]
public class SSFireRate : Modifier
{
    public override void Apply()
    {
        PlayerStats.Instance.SSAddSpeed(1);
    }
}
