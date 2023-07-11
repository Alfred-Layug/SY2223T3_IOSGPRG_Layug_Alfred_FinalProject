using System.Collections;
using System.Collections.Generic;
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

    public override void Shoot()
    {
        if (!_isReloading)
        {
            base.Shoot();
            _currentGun.Shoot(_bulletPrefab, _nozzle);
            _inventoryScript.ExpendAmmo();
            Debug.Log($"{_name} is shooting");
        }
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
}
