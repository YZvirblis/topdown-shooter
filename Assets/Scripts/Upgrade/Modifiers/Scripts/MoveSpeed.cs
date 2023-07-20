using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Modifiers/Move Speed Buff" +
    "")]
public class MoveSpeed : Modifier
{
    public float amount;
    public override void Apply()
    {
        PlayerStats.Instance.moveSpeed += amount;
    }
}
