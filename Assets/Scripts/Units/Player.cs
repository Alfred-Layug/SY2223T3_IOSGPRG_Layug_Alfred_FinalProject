using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : GameUnit
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _nozzle;
    [SerializeField] private Image _shootButton;
    public bool _isReloading;
    private bool _isFiring;
    private bool _stopFiring;
    private bool _canFire;
    private Inventory _inventoryScript;

    private void Start()
    {
        base.Initialize("Austin", 100, 0.2f);
        _isReloading = false;
        _isFiring = false;
        _stopFiring = false;
        _canFire = true;
        _inventoryScript = this.gameObject.GetComponent<Inventory>();
    }

    private void Update()
    {
        if (_isFiring && _currentGun._canShoot == true)
        {
            Shoot();
            MakeIsFiringFalse();
        }
    }

    public void OnPointerDown()
    {
        _stopFiring = false;
        MakeIsFiringTrue();
    }

    public void OnPointerUp()
    {
        _isFiring = false;
        _stopFiring = true;
        _canFire = true;
    }

    private void MakeIsFiringTrue()
    {
        _isFiring = true;
    }

    private void MakeIsFiringFalse()
    {
        _isFiring = false;
        StartCoroutine(ShootNextBullet(_currentGun._fireRate));
    }

    public override void Shoot()
    {
        base.Shoot();
        if (!_isReloading && _currentGun._canShoot == true && _currentGun == _inventoryScript._gunTypes[1])
        {
            _currentGun.Shoot(_bulletPrefab, _nozzle);
            _currentGun._canShoot = false;
            _inventoryScript.ExpendAmmo();
        }
        else if (!_isReloading && _currentGun._canShoot == true && _canFire)
        {
            _currentGun.Shoot(_bulletPrefab, _nozzle);
            _currentGun._canShoot = false;
            _inventoryScript.ExpendAmmo();
            _canFire = false;
        }
        Debug.Log($"{_name} is shooting");
    }

    public void SetCurrentGun(Gun gun, int ammoCount)
    {
        _currentGun = gun;
        UIManager.instance.UpdateCurrentWeaponAmmoCount(_currentGun, ammoCount);
    }

    public float GetSpeed()
    {
        return _speed;
    }

    private IEnumerator ShootNextBullet(float fireRate)
    {
        yield return new WaitForSeconds(fireRate);
        _currentGun._canShoot = true;
        if (!_stopFiring)
        {
            MakeIsFiringTrue();
        }
    }
}
