using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    private void Start()
    {
        _canShoot = true;
        _fireRate = 0.6f;
        _reloadTime = 2.7f;
        _bulletSpread = 10f;
    }

    public override void Shoot(GameObject prefab, GameObject nozzle)
    {
        if (UIManager.instance._currentShotgunMagazineAmmo > 0 && _canShoot)
        {
            for (int i = 0; i < 8; i++)
            {
                GameObject b = Instantiate(prefab, nozzle.transform.position, nozzle.transform.rotation);
                Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
                Vector2 dir = transform.rotation * Vector2.up;
                Vector2 pdir = Vector2.Perpendicular(dir) * Random.Range(-_bulletSpread, _bulletSpread);
                rb.velocity = (dir + pdir);
            }
            _canShoot = false;
            Debug.Log("Cone Shot");
        }
    }
}
