using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AutomaticRifle : Gun
{
    public Inventory _inventoryScript;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _nozzle;

    private void Start()
    {
        _damage = 15;
        _canShoot = true;
        _isFiring = false;
        _stopFiring = false;
        _fireRate = 0.35f;
        _reloadTime = 2.3f;
        _bulletSpread = 1.1f;
        _currentMagazineAmmo = 30;
    }

    private void Update()
    {
        if (_isFiring)
        {
            MakeIsFiringFalse();
            Shoot(_bulletPrefab, _nozzle);
        }
    }

    public override void Shoot(GameObject prefab, GameObject nozzle)
    {
        if ((_inventoryScript != null && _inventoryScript._currentMagazineAmmo[(int)Weapon.AutomaticRifle] > 0 && _canShoot)
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
                _inventoryScript.ExpendAmmo(Weapon.AutomaticRifle);
            }
            else
            {
                _currentMagazineAmmo--;
                if (_currentMagazineAmmo > 0)
                {
                    StartCoroutine(AutomaticRifleFireRateTimer());
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
        if (!_stopFiring)
        {
            _canShoot = true;
            MakeIsFiringTrue();
        }
    }

    private IEnumerator AutomaticRifleFireRateTimer()
    {
        yield return new WaitForSeconds(_fireRate);
        _canShoot = true;
    }

    public override IEnumerator EnemyReload()
    {
        Debug.Log("Enemy is reloading assault rifle");
        yield return new WaitForSeconds(_reloadTime * 2f);
        _currentMagazineAmmo = 30;
        _canShoot = true;
        Debug.Log("Enemy finished reloading assault rifle");
    }

    public override void OnPointerDown()
    {
        base.OnPointerDown();
        _stopFiring = false;
        MakeIsFiringTrue();
    }

    public override void OnPointerUp()
    {
        base.OnPointerUp();
        _isFiring = false;
        _stopFiring = true;
    }

    private void MakeIsFiringTrue()
    {
        _isFiring = true;
    }

    private void MakeIsFiringFalse()
    {
        _isFiring = false;
        StartCoroutine(FireRateTimer());
    }
}
