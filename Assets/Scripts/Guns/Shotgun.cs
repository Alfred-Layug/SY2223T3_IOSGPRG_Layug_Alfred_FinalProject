using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    public Inventory _inventoryScript;
    private bool _triggerReleased;

    private void Start()
    {
        _damage = 10;
        _canShoot = true;
        _triggerReleased = true;
        _fireRate = 0.6f;
        _reloadTime = 2.7f;
        _bulletSpread = 10f;
        _currentMagazineAmmo = 2;
    }

    public override void Shoot(GameObject prefab, GameObject nozzle)
    {
        if ((_inventoryScript != null && _inventoryScript._currentMagazineAmmo[(int)Weapon.Shotgun] > 0 && _canShoot && _triggerReleased)
            || (_isEnemy && _canShoot && _currentMagazineAmmo > 0))
        {
            for (int i = 0; i < 8; i++)
            {
                GameObject bullet = Instantiate(prefab, nozzle.transform.position, nozzle.transform.rotation);
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                bulletScript.SetBulletDamage(_damage);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                Vector2 dir = transform.rotation * Vector2.up;
                Vector2 perpendicularDir = Vector2.Perpendicular(dir) * Random.Range(-_bulletSpread, _bulletSpread);
                rb.velocity = (dir + perpendicularDir);
            }
            _canShoot = false;
            if (_inventoryScript != null)
            {
                StartCoroutine(FireRateTimer());
                _inventoryScript.ExpendAmmo(Weapon.Shotgun);
            }
            else
            {
                _currentMagazineAmmo--;
                if (_currentMagazineAmmo > 0)
                {
                    StartCoroutine(FireRateTimer());
                }
                else
                {
                    StartCoroutine(EnemyReload());
                }
            }
        }
    }

    public override IEnumerator FireRateTimer()
    {
        yield return new WaitForSeconds(_fireRate);
        _canShoot = true;
    }

    public override IEnumerator EnemyReload()
    {
        Debug.Log("Enemy is reloading shotgun");
        yield return new WaitForSeconds(_reloadTime * 2f);
        _currentMagazineAmmo = 2;
        _canShoot = true;
        Debug.Log("Enemy finished reloading shotgun");
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
