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
    private Inventory _inventoryScript;

    private void Start()
    {
        base.Initialize("Austin", 100, 0.2f);
        _isReloading = false;
        _inventoryScript = this.gameObject.GetComponent<Inventory>();
    }

    public void OnPointerDown()
    {
        if (!_isReloading && _currentGun != _inventoryScript._gunTypes[(int)Weapon.AutomaticRifle])
        {
            Shoot();
        }
        _currentGun.OnPointerDown();
    }

    public void OnPointerUp()
    {
        _currentGun.OnPointerUp();
    }

    public override void Shoot()
    {
        base.Shoot();
        _currentGun.Shoot(_bulletPrefab, _nozzle);
    }

    public void SetCurrentGun(Gun gun)
    {
        _currentGun = gun;
        _currentGun._canShoot = true;
    }

    public float GetSpeed()
    {
        return _speed;
    }
}
