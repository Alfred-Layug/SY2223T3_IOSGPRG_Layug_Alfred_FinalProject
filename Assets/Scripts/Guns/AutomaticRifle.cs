using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticRifle : Gun
{
    public override void Shoot(GameObject prefab, GameObject nozzle)
    {
        if (UIManager.instance._currentAutomaticRifleMagazineAmmo > 0)
        {
            Instantiate(prefab, nozzle.transform.position, nozzle.transform.rotation);
            Debug.Log("Multi-Shot");
        }
    }
}
