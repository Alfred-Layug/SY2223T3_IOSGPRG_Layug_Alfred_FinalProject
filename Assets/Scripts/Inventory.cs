using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private int _pistolAmmoMaxCarry;
    [SerializeField] private int _automaticRifleAmmoMaxCarry;
    [SerializeField] private int _shotgunAmmoMaxCarry;

    private int _pistolAmmoCarry;
    private int _automaticRifleAmmoCarry;
    private int _shotgunAmmoCarry;
    private int _healthKits;
    private Gun _primaryWeapon;
    private Gun _secondaryWeapon;

    private void Start()
    {
        _pistolAmmoMaxCarry = 90;
        _automaticRifleAmmoMaxCarry = 120;
        _shotgunAmmoMaxCarry = 60;
        _pistolAmmoCarry = 0;
        _automaticRifleAmmoCarry = 0;
        _shotgunAmmoCarry = 0;
    }

    public void ChangeWeapon(WeaponSlot weaponSlot)
    {
        if (weaponSlot == WeaponSlot.Primary)
        {
            _player.SetCurrentGun(_primaryWeapon);
        }
    }

    public void ReloadCurrentGun()
    {

    }

    public void UseHealthKit()
    {

    }

    public void AddAmmo(AmmoType ammoType)
    {
        if (ammoType == AmmoType.PistolAmmo && _pistolAmmoCarry < _pistolAmmoMaxCarry)
        {
            int ammoAmmount = Random.Range(1, 9);
            _pistolAmmoCarry += ammoAmmount;
            _pistolAmmoCarry = Mathf.Min(_pistolAmmoCarry, _pistolAmmoMaxCarry);
            UIManager.instance.UpdateAmmoCount(ammoType, ammoAmmount);
        }
        else if (ammoType == AmmoType.AutomaticRifleAmmo && _automaticRifleAmmoCarry < _automaticRifleAmmoMaxCarry)
        {
            int ammoAmmount = Random.Range(5, 16);
            _automaticRifleAmmoCarry += ammoAmmount;
            _automaticRifleAmmoCarry = Mathf.Min(_automaticRifleAmmoCarry, _automaticRifleAmmoMaxCarry);
            UIManager.instance.UpdateAmmoCount(ammoType, ammoAmmount);
        }
        else if (ammoType == AmmoType.ShotgunAmmo && _shotgunAmmoCarry < _shotgunAmmoMaxCarry)
        {
            int ammoAmmount = Random.Range(1, 3);
            _shotgunAmmoCarry += ammoAmmount;
            _shotgunAmmoCarry = Mathf.Min(_shotgunAmmoCarry, _shotgunAmmoMaxCarry);
            UIManager.instance.UpdateAmmoCount(ammoType, ammoAmmount);
        }
    }
}
