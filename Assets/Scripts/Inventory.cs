using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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
    private Weapon _weapon1;
    private Weapon _weapon2;
    public Gun _primaryWeapon;
    public Gun _secondaryWeapon;
    public bool _primaryWeaponSelected;

    public int _currentPistolMagazineAmmo;
    public int _currentPistolBagAmmo;
    public int _currentAutomaticRifleMagazineAmmo;
    public int _currentAutomaticRifleBagAmmo;
    public int _currentShotgunMagazineAmmo;
    public int _currentShotgunBagAmmo;

    private void Start()
    {
        _pistolAmmoCarry = 0;
        _automaticRifleAmmoCarry = 0;
        _shotgunAmmoCarry = 0;
        _primaryWeaponSelected = true;

        _currentPistolMagazineAmmo = 0;
        _currentAutomaticRifleMagazineAmmo = 0;
        _currentShotgunMagazineAmmo = 0;
    }

    public void ChangeWeapon(int weaponSlot)
    {
        if (weaponSlot == (int)WeaponSlot.Primary && _primaryWeapon != null && _player._isReloading == false)
        {
            HideWeapons();
            _player.SetCurrentGun(_primaryWeapon);
            ShowWeapon(_weapon1);
            if (_weapon1 == Weapon.AutomaticRifle)
            {
                UIManager.instance.UpdateCurrentWeaponAmmoCount(_automaticRifleAmmoCarry, _currentAutomaticRifleMagazineAmmo, _currentAutomaticRifleBagAmmo);
            }
            else if (_weapon1 == Weapon.Shotgun)
            {
                UIManager.instance.UpdateCurrentWeaponAmmoCount(_shotgunAmmoCarry, _currentShotgunMagazineAmmo, _currentShotgunBagAmmo);
            }
            _primaryWeaponSelected = true;
        }
        else if (weaponSlot == (int)WeaponSlot.Secondary && _secondaryWeapon != null && _player._isReloading == false)
        {
            HideWeapons();
            _player.SetCurrentGun(_secondaryWeapon);
            ShowWeapon(_weapon2);
            UIManager.instance.UpdateCurrentWeaponAmmoCount(_pistolAmmoCarry, _currentPistolMagazineAmmo, _currentPistolBagAmmo);
            _primaryWeaponSelected = false;
        }
    }

    public IEnumerator ReloadCurrentGun()
    {
        _player._isReloading = true;
        yield return new WaitForSeconds(_player._currentGun._reloadTime);
        if (_player._currentGun == _gunTypes[(int)Weapon.Pistol])
        {
            _currentPistolMagazineAmmo = Mathf.Min(15, _currentPistolMagazineAmmo + _currentPistolBagAmmo);
            _currentPistolBagAmmo = Mathf.Max(0, _pistolAmmoCarry - _currentPistolMagazineAmmo);
            UIManager.instance.UpdateCurrentWeaponAmmoCount(_pistolAmmoCarry, _currentPistolMagazineAmmo, _currentPistolBagAmmo);
        }
        else if (_player._currentGun == _gunTypes[(int)Weapon.AutomaticRifle])
        {
            _currentAutomaticRifleMagazineAmmo = Mathf.Min(30, _currentAutomaticRifleMagazineAmmo + _currentAutomaticRifleBagAmmo);
            _currentAutomaticRifleBagAmmo = Mathf.Max(0, _automaticRifleAmmoCarry - _currentAutomaticRifleMagazineAmmo);
            UIManager.instance.UpdateCurrentWeaponAmmoCount(_automaticRifleAmmoCarry, _currentAutomaticRifleMagazineAmmo, _currentAutomaticRifleBagAmmo);
        }
        else if (_player._currentGun == _gunTypes[(int)Weapon.Shotgun])
        {
            _currentShotgunMagazineAmmo = Mathf.Min(2, _currentShotgunMagazineAmmo + _currentShotgunBagAmmo);
            _currentShotgunBagAmmo = Mathf.Max(0, _shotgunAmmoCarry - _currentShotgunMagazineAmmo);
            UIManager.instance.UpdateCurrentWeaponAmmoCount(_shotgunAmmoCarry, _currentShotgunMagazineAmmo, _currentShotgunBagAmmo);
        }

        _player._isReloading = false;
        UIManager.instance._reloadingText.enabled = false;
    }

    public void UseHealthKit()
    {

    }

    public void AddAmmo(Weapon weapon)
    {
        if (weapon == Weapon.Pistol && _pistolAmmoCarry < _pistolAmmoMaxCarry)
        {
            int ammoAmmount = Random.Range(1, 9);
            _pistolAmmoCarry += ammoAmmount;
            _pistolAmmoCarry = Mathf.Min(_pistolAmmoCarry, _pistolAmmoMaxCarry);
            _currentPistolBagAmmo = Mathf.Max(0, _pistolAmmoCarry - _currentPistolMagazineAmmo);
            UIManager.instance.UpdateAmmoCount(weapon, _pistolAmmoCarry, _currentPistolBagAmmo, _currentPistolMagazineAmmo);
        }
        else if (weapon == Weapon.AutomaticRifle && _automaticRifleAmmoCarry < _automaticRifleAmmoMaxCarry)
        {
            int ammoAmmount = Random.Range(5, 16);
            _automaticRifleAmmoCarry += ammoAmmount;
            _automaticRifleAmmoCarry = Mathf.Min(_automaticRifleAmmoCarry, _automaticRifleAmmoMaxCarry);
            _currentAutomaticRifleBagAmmo = Mathf.Max(0, _automaticRifleAmmoCarry - _currentAutomaticRifleMagazineAmmo);
            UIManager.instance.UpdateAmmoCount(weapon, _automaticRifleAmmoCarry, _currentAutomaticRifleBagAmmo, _currentAutomaticRifleMagazineAmmo);
        }
        else if (weapon == Weapon.Shotgun && _shotgunAmmoCarry < _shotgunAmmoMaxCarry)
        {
            int ammoAmmount = Random.Range(1, 3);
            _shotgunAmmoCarry += ammoAmmount;
            _shotgunAmmoCarry = Mathf.Min(_shotgunAmmoCarry, _shotgunAmmoMaxCarry);
            _currentShotgunBagAmmo = Mathf.Max(0, _shotgunAmmoCarry - _currentShotgunMagazineAmmo);
            UIManager.instance.UpdateAmmoCount(weapon, _shotgunAmmoCarry, _currentShotgunBagAmmo, _currentShotgunMagazineAmmo);
        }
    }

    public void LootWeapon(Weapon weapon)
    {
        if (weapon == Weapon.Pistol)
        {
            _secondaryWeapon = _gunTypes[(int)weapon];
            _weapon2 = weapon;
            
            if (!_primaryWeaponSelected)
            {
                ShowWeapon(weapon);  //Show pistol
                _player.SetCurrentGun(_secondaryWeapon);
                UIManager.instance.UpdateCurrentWeaponAmmoCount(_pistolAmmoCarry, _currentPistolMagazineAmmo, _currentPistolBagAmmo);
            }
        }
        else if (weapon == Weapon.AutomaticRifle && _primaryWeapon != _gunTypes[(int)weapon])
        {
            _primaryWeapon = _gunTypes[(int)weapon];
            _weapon1 = weapon;

            if (_primaryWeaponSelected)
            {
                ShowWeapon(weapon);  //Show automatic rifle
                _player.SetCurrentGun(_primaryWeapon);
                UIManager.instance.UpdateAmmoCount(weapon, _automaticRifleAmmoCarry, _currentAutomaticRifleBagAmmo, _currentAutomaticRifleMagazineAmmo);
            }
        }
        else if (weapon == Weapon.Shotgun && _primaryWeapon != _gunTypes[(int)weapon])
        {
            _primaryWeapon = _gunTypes[(int)weapon];
            _weapon1 = weapon;

            if (_primaryWeaponSelected)
            {
                ShowWeapon(weapon);  //Show shotgun
                _player.SetCurrentGun(_primaryWeapon);
                UIManager.instance.UpdateAmmoCount(weapon, _shotgunAmmoCarry, _currentShotgunBagAmmo, _currentShotgunMagazineAmmo);
            }
        }

        UIManager.instance.UpdateWeaponSlotText(weapon);
    }

    public void ShowWeapon(Weapon weapon)
    {
        HideWeapons();   
        _weapons[(int)weapon].SetActive(true);
    }

    public void HideWeapons()
    {
        foreach (GameObject weaponGO in _weapons)
        {
            weaponGO.SetActive(false);
        }
    }

    public void ExpendAmmo()
    {
        if (!_primaryWeaponSelected && _secondaryWeapon == _gunTypes[0] &&
                _currentPistolMagazineAmmo > 0)
        {
            _pistolAmmoCarry--;
            _currentPistolMagazineAmmo--;
            UIManager.instance.UpdateAmmoCount(Weapon.Pistol, _pistolAmmoCarry, _currentPistolBagAmmo, _currentPistolMagazineAmmo);
        }

        if (_primaryWeaponSelected && _primaryWeapon == _gunTypes[1] &&
                _currentAutomaticRifleMagazineAmmo > 0)
        {
            _automaticRifleAmmoCarry--;
            _currentAutomaticRifleMagazineAmmo--;
            UIManager.instance.UpdateAmmoCount(Weapon.AutomaticRifle, _automaticRifleAmmoCarry, _currentAutomaticRifleBagAmmo, _currentAutomaticRifleMagazineAmmo);
        }
        else if (_primaryWeaponSelected && _primaryWeapon == _gunTypes[2] &&
                _currentShotgunMagazineAmmo > 0)
        {
            _shotgunAmmoCarry--;
            _currentShotgunMagazineAmmo--;
            UIManager.instance.UpdateAmmoCount(Weapon.Shotgun, _shotgunAmmoCarry, _currentShotgunBagAmmo, _currentShotgunMagazineAmmo);
        }
    }
}
