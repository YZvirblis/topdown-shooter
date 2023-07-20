using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Modifiers/SS Speed Buff" +
    "")]
public class SSSpeed : Modifier
{
    public override void Apply()
    {
        PlayerStats.Instance.SSAddSpeed(45);
    }
}
