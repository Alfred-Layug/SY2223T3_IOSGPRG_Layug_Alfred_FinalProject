using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : GameUnit
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _rocketPrefab;
    [SerializeField] private GameObject _nozzle;
    [SerializeField] private Image _shootButton;
    public bool _isReloading;
    private Inventory _inventoryScript;

    private void Start()
    {
        base.Initialize("Austin", 100, 0.12f);
        Spawner.instance._units.Add(this);
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
        if (_currentGun.GetComponent<RocketLauncher>() != null)
        {
            _currentGun.Shoot(_rocketPrefab, _nozzle);
        }
        else
        {
            _currentGun.Shoot(_bulletPrefab, _nozzle);
        }
    }

    public void SetCurrentGun(Gun gun)
    {
        _currentGun = gun;
        _currentGun._canShoot = true;
    }

    public override void DoDeath()
    {
        this.GetComponent<PlayerMovement>().enabled = false;
        this.GetComponent<CircleCollider2D>().enabled = false;
        UIManager.instance._gameplayCanvas.SetActive(false);
        UIManager.instance._gameOverCanvas.SetActive(true);
        Spawner.instance.DecreaseUnitCount(this);
    }
}
