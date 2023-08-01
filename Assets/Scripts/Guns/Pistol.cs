using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    public Inventory _inventoryScript;
    private bool _triggerReleased;

    private void Start()
    {
        _damage = 10;
        _canShoot = true;
        _triggerReleased = true;
        _fireRate = 2.16f;
        _reloadTime = 2f;
        _bulletSpread = 1.2f;
        _currentMagazineAmmo = 15;
    }

    public override void Shoot(GameObject prefab, GameObject nozzle)
    {
        if ((_inventoryScript != null && _inventoryScript._currentMagazineAmmo[(int)Weapon.Pistol] > 0 && _canShoot && _triggerReleased)
            || (_isEnemy && _canShoot && _currentMagazineAmmo > 0))
        {
            GameObject bullet = Instantiate(prefab, nozzle.transform.position, nozzle.transform.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.SetBulletDamage(_damage);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector2 dir = transform.rotation * Vector2.up;
            Vector2 perpendicularDir = Vector2.Perpendicular(dir) * Random.Range(-_bulletSpread, _bulletSpread);
            rb.velocity = (dir + perpendicularDir);
            _canShoot = false;

            if (_inventoryScript != null)
            {
                StartCoroutine(FireRateTimer());
                _inventoryScript.ExpendAmmo(Weapon.Pistol);
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
        Debug.Log("Enemy is reloading pistol");
        yield return new WaitForSeconds(_reloadTime * 2f);
        _currentMagazineAmmo = 15;
        _canShoot = true;
        Debug.Log("Enemy finished reloading pistol");
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
