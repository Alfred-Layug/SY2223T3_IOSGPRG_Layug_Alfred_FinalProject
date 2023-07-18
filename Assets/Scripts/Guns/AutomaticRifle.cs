using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticRifle : Gun
{
    private void Start()
    {
        _canShoot = true;
        _fireRate = 0.35f;
        _reloadTime = 2.3f;
        _bulletSpread = 1.1f;
    }

    public override void Shoot(GameObject prefab, GameObject nozzle)
    {
        if (UIManager.instance._currentAutomaticRifleMagazineAmmo > 0 && _canShoot)
        {
            GameObject b = Instantiate(prefab, nozzle.transform.position, nozzle.transform.rotation);
            Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
            Vector2 dir = transform.rotation * Vector2.up;
            Vector2 pdir = Vector2.Perpendicular(dir) * Random.Range(-_bulletSpread, _bulletSpread);
            rb.velocity = (dir + pdir);
            _canShoot = false;
            Debug.Log("Multi-Shot");
        }
    }
}
