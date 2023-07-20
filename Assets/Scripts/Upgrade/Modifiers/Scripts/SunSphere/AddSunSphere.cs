using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Modifiers/Add Sun Sphere" +
    "")]
public class AddSunSphere : Modifier
{
    public override void Apply()
    {
        PlayerStats.Instance.sunSphere.SetActive(true);
        PlayerStats.Instance.sunSphereActive = true;
    }
}
