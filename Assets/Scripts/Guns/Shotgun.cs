using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    public override void Shoot(GameObject prefab, GameObject nozzle)
    {
        if (UIManager.instance._currentShotgunMagazineAmmo > 0)
        {
            Instantiate(prefab, nozzle.transform.position, nozzle.transform.rotation);
            Debug.Log("Cone Shot");
        }
    }
}
