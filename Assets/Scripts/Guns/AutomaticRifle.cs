using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticRifle : Gun
{
    public override void Shoot()
    {
        base.Shoot();
        Debug.Log("Multi-Shot");
    }
}
