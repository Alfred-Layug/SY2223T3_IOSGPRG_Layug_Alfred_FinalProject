using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    [SerializeField] private int _pistolAmmoMaxCarry;
    [SerializeField] private int _automaticRifleAmmoMaxCarry;
    [SerializeField] private int _shotgunAmmoMaxCarry;
    [SerializeField] private List<GameObject> _weapons;
    public Player _player;
    public List<Gun> _gunTypes;

    private int _pistolAmmoCarry;
    private int _automaticRifleAmmoCarry;
    private int _shotgunAmmoCarry;
    private int _healthKits;
    public Gun _primaryWeapon;
    public Gun _secondaryWeapon;
    public bool _primaryWeaponSelected;

    private void Start()
    {
        _pistolAmmoMaxCarry = 90;
        _automaticRifleAmmoMaxCarry = 120;
        _shotgunAmmoMaxCarry = 60;
        _pistolAmmoCarry = 0;
        _automaticRifleAmmoCarry = 0;
        _shotgunAmmoCarry = 0;
        _primaryWeaponSelected = true;
    }

    public void ChangeToPrimaryWeapon()
    {
        if (_primaryWeapon == _gunTypes[1])
        {
            _player.SetCurrentGun(_primaryWeapon, _automaticRifleAmmoCarry);
            ShowWeapon(1);
        }
        else if (_primaryWeapon == _gunTypes[2])
        {
            _player.SetCurrentGun(_primaryWeapon, _shotgunAmmoCarry);
            ShowWeapon(2);
        }
        else
        {
            HideWeapons();
        }

        _primaryWeaponSelected = true;
    }

    public void ChangeToSecondaryWeapon()
    {
        if (_secondaryWeapon == _gunTypes[0])
        {
            _player.SetCurrentGun(_secondaryWeapon, _pistolAmmoCarry);
            ShowWeapon(0);
        }
        else
        {
            HideWeapons();
        }

        _primaryWeaponSelected = false;
    }

    public IEnumerator ReloadCurrentGun()
    {
        _player._isReloading = true;
        yield return new WaitForSeconds(_player._currentGun._reloadTime);
        if (_player._currentGun == _gunTypes[0])
        {
            UIManager.instance._currentPistolMagazineAmmo = Mathf.Min(15, _pistolAmmoCarry);
            UIManager.instance._currentPistolBagAmmo = Mathf.Max(0, _pistolAmmoCarry - UIManager.instance._currentPistolMagazineAmmo);
            UIManager.instance.UpdateCurrentWeaponAmmoCount(_player._currentGun, _pistolAmmoCarry);
        }
        else if (_player._currentGun == _gunTypes[1])
        {
            UIManager.instance._currentAutomaticRifleMagazineAmmo = Mathf.Min(30, _automaticRifleAmmoCarry);
            UIManager.instance._currentAutomaticRifleBagAmmo = Mathf.Max(0, _automaticRifleAmmoCarry - UIManager.instance._currentAutomaticRifleMagazineAmmo);
            UIManager.instance.UpdateCurrentWeaponAmmoCount(_player._currentGun, _automaticRifleAmmoCarry);
        }
        else if (_player._currentGun == _gunTypes[2])
        {
            UIManager.instance._currentShotgunMagazineAmmo = Mathf.Min(2, _shotgunAmmoCarry);
            UIManager.instance._currentShotgunBagAmmo = Mathf.Max(0, _shotgunAmmoCarry - UIManager.instance._currentShotgunMagazineAmmo);
            UIManager.instance.UpdateCurrentWeaponAmmoCount(_player._currentGun, _shotgunAmmoCarry);
        }

        _player._isReloading = false;
        UIManager.instance._reloadingText.enabled = false;
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

    public void LootWeapon(Weapon weapon)
    {
        if (weapon == Weapon.Pistol)
        {
            _secondaryWeapon = _gunTypes[0];
            
            if (!_primaryWeaponSelected)
            {
                ShowWeapon(0);  //Show pistol
                _player.SetCurrentGun(_secondaryWeapon, _pistolAmmoCarry);
            }
        }
        else if (weapon == Weapon.AutomaticRifle && _primaryWeapon != _gunTypes[1])
        {
            _primaryWeapon = _gunTypes[1];

            if (_primaryWeaponSelected)
            {
                ShowWeapon(1);  //Show automatic rifle
                _player.SetCurrentGun(_primaryWeapon, _automaticRifleAmmoCarry);
            }
        }
        else if (weapon == Weapon.Shotgun && _primaryWeapon != _gunTypes[2])
        {
            _primaryWeapon = _gunTypes[2];

            if (_primaryWeaponSelected)
            {
                ShowWeapon(2);  //Show shotgun
                _player.SetCurrentGun(_primaryWeapon, _shotgunAmmoCarry);
            }
        }

        UIManager.instance.UpdateWeaponSlotText(weapon);
    }

    public void ShowWeapon(int weaponType)
    {
        for (int i = 0; i < 3; i++)
        {
            if (weaponType == i)
            {
                _weapons[i].SetActive(true);
            }
            else
            {
                _weapons[i].SetActive(false);
            }
        }
    }

    public void HideWeapons()
    {
        for (int i = 0; i < 3; i++)
        {
            _weapons[i].SetActive(false);
        }
    }

    public void ExpendAmmo()
    {
        if (!_primaryWeaponSelected && _secondaryWeapon == _gunTypes[0] &&
                UIManager.instance._currentPistolMagazineAmmo > 0)
        {
            _pistolAmmoCarry--;
            UIManager.instance._currentPistolMagazineAmmo--;
            UIManager.instance.UpdateAmmoCount(AmmoType.PistolAmmo, -1);
        }

        if (_primaryWeaponSelected && _primaryWeapon == _gunTypes[1] &&
                UIManager.instance._currentAutomaticRifleMagazineAmmo > 0)
        {
            _automaticRifleAmmoCarry--;
            UIManager.instance._currentAutomaticRifleMagazineAmmo--;
            UIManager.instance.UpdateAmmoCount(AmmoType.AutomaticRifleAmmo, -1);
        }
        else if (_primaryWeaponSelected && _primaryWeapon == _gunTypes[2] &&
                UIManager.instance._currentShotgunMagazineAmmo > 0)
        {
            _shotgunAmmoCarry--;
            UIManager.instance._currentShotgunMagazineAmmo--;
            UIManager.instance.UpdateAmmoCount(AmmoType.ShotgunAmmo, -1);
        }
    }
}
