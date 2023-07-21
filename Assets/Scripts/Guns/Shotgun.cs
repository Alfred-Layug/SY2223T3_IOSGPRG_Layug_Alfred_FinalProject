using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    public Inventory _inventoryScript;
    private bool _triggerReleased;

    private void Start()
    {
        _canShoot = true;
        _triggerReleased = true;
        _fireRate = 0.6f;
        _reloadTime = 2.7f;
        _bulletSpread = 10f;
    }

    public override void Shoot(GameObject prefab, GameObject nozzle)
    {
        if (_inventoryScript._currentShotgunMagazineAmmo > 0 && _canShoot && _triggerReleased)
        {
            for (int i = 0; i < 8; i++)
            {
                GameObject bullet = Instantiate(prefab, nozzle.transform.position, nozzle.transform.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                Vector2 dir = transform.rotation * Vector2.up;
                Vector2 perpendicularDir = Vector2.Perpendicular(dir) * Random.Range(-_bulletSpread, _bulletSpread);
                rb.velocity = (dir + perpendicularDir);
            }
            _canShoot = false;
            StartCoroutine(FireRateTimer());
            _inventoryScript.ExpendAmmo();
            Debug.Log("Cone Shot");
        }
    }

    public override IEnumerator FireRateTimer()
    {
        yield return new WaitForSeconds(_fireRate);
        _canShoot = true;
    }

    public override void OnPointerDown()
    {
        base.OnPointerDown();
        _triggerReleased = false;
    }

    public override void OnPointerUp()
    {
        base.OnPointerUp();
        _triggerReleased = true;
    }
}
