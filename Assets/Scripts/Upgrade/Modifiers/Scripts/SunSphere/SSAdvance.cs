using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Modifiers/SS Advance Stage" +
    "")]
public class SSAdvance : Modifier
{
    public override void Apply()
    {
        PlayerStats.Instance.SSAdvanceStage();
    }
}
