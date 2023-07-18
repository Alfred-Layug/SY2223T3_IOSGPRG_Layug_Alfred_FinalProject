using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    private void Start()
    {
        _canShoot = true;
        _fireRate = 2.16f;
        _reloadTime = 2f;
        _bulletSpread = 1.2f;
    }

    public override void Shoot(GameObject prefab, GameObject nozzle)
    {
        if (UIManager.instance._currentPistolMagazineAmmo > 0 && _canShoot)
        {
            GameObject b = Instantiate(prefab, nozzle.transform.position, nozzle.transform.rotation);
            Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
            Vector2 dir = transform.rotation * Vector2.up;
            Vector2 pdir = Vector2.Perpendicular(dir) * Random.Range(-_bulletSpread, _bulletSpread);
            rb.velocity = (dir + pdir);
            _canShoot = false;
            Debug.Log("Single Shot");
        }
    }
}
