using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<int> _maxAmmoCarry;
    [SerializeField] private List<GameObject> _weapons;
    public Player _player;
    public List<Gun> _gunTypes;

    private int _healthKits;
    private Weapon _weapon1;
    private Weapon _weapon2;
    private Weapon _currentWeapon;
    public Gun _primaryWeapon;
    public Gun _secondaryWeapon;
    public bool _primaryWeaponSelected;

    public List<int> _ammoCarry;
    public List<int> _currentMagazineAmmo;
    public List<int> _currentBagAmmo;

    private void Start()
    {
        _primaryWeaponSelected = true;
        for (int i =  0; i < 4; i++)
        {
            _ammoCarry.Add(0);
            _currentMagazineAmmo.Add(0);
            _currentBagAmmo.Add(0);
        }
        _currentBagAmmo[(int)Weapon.RocketLauncher] = 10;
    }

    public void ChangeWeapon(int weaponSlot)
    {
        if (weaponSlot == (int)WeaponSlot.Primary && _primaryWeapon != null && _player._isReloading == false)
        {
            HideWeapons();
            _player.SetCurrentGun(_primaryWeapon);
            _currentWeapon = _weapon1;
            ShowWeapon(_weapon1);
            UIManager.instance.UpdateCurrentWeaponAmmoCount(_currentMagazineAmmo[(int)_weapon1], _currentBagAmmo[(int)_weapon1]);
            _primaryWeaponSelected = true;
        }
        else if (weaponSlot == (int)WeaponSlot.Secondary && _secondaryWeapon != null && _player._isReloading == false)
        {
            HideWeapons();
            _player.SetCurrentGun(_secondaryWeapon);
            _currentWeapon = _weapon2;
            ShowWeapon(_weapon2);
            UIManager.instance.UpdateCurrentWeaponAmmoCount(_currentMagazineAmmo[(int)_weapon2], _currentBagAmmo[(int)_weapon2]);
            _primaryWeaponSelected = false;
        }
    }

    public IEnumerator ReloadCurrentGun()
    {
        _player._isReloading = true;
        yield return new WaitForSeconds(_player._currentGun._reloadTime);
        if (_player._currentGun == _gunTypes[(int)Weapon.Pistol])
        {
            _currentMagazineAmmo[(int)_currentWeapon] = Mathf.Min(15, _currentMagazineAmmo[(int)_currentWeapon] + _currentBagAmmo[(int)_currentWeapon]);
        }
        else if (_player._currentGun == _gunTypes[(int)Weapon.AutomaticRifle])
        {
            _currentMagazineAmmo[(int)_currentWeapon] = Mathf.Min(30, _currentMagazineAmmo[(int)_currentWeapon] + _currentBagAmmo[(int)_currentWeapon]);
        }
        else if (_player._currentGun == _gunTypes[(int)Weapon.Shotgun])
        {
            _currentMagazineAmmo[(int)_currentWeapon] = Mathf.Min(2, _currentMagazineAmmo[(int)_currentWeapon] + _currentBagAmmo[(int)_currentWeapon]);
        }
        else if (_player._currentGun == _gunTypes[(int)Weapon.RocketLauncher])
        {
            _currentMagazineAmmo[(int)_currentWeapon] = Mathf.Min(1, _currentBagAmmo[(int)_currentWeapon]);
        }

        if (_player._currentGun == _gunTypes[(int)Weapon.RocketLauncher])
        {
            _currentBagAmmo[(int)_currentWeapon] = Mathf.Max(0, _currentBagAmmo[(int)_currentWeapon] - 1);
        }
        else
        {
            _currentBagAmmo[(int)_currentWeapon] = Mathf.Max(0, _ammoCarry[(int)_currentWeapon] - _currentMagazineAmmo[(int)_currentWeapon]);
        }
        UIManager.instance.UpdateCurrentWeaponAmmoCount(_currentMagazineAmmo[(int)_currentWeapon], _currentBagAmmo[(int)_currentWeapon]);
        _player._isReloading = false;
        UIManager.instance._reloadingText.enabled = false;
    }

    public void UseHealthKit()
    {

    }

    public void AddAmmo(Weapon weapon, int amount)
    {
        _ammoCarry[(int)weapon] += amount;
        _ammoCarry[(int)weapon] = Mathf.Min(_ammoCarry[(int)weapon], _maxAmmoCarry[(int)weapon]);
        _currentBagAmmo[(int)weapon] = Mathf.Max(0, _ammoCarry[(int)weapon] - _currentMagazineAmmo[(int)weapon]);
        UIManager.instance.UpdateAmmoCount(weapon, _ammoCarry[(int)weapon], _currentBagAmmo[(int)weapon], _currentMagazineAmmo[(int)weapon]);
    }

    public void LootWeapon(Weapon weapon)
    {
        if (weapon == Weapon.Pistol)
        {
            _secondaryWeapon = _gunTypes[(int)weapon];
            _weapon2 = weapon;
            
            if (!_primaryWeaponSelected)
            {
                ShowWeapon(weapon);  //Show secondary weapon
                _player.SetCurrentGun(_secondaryWeapon);
                _currentWeapon = _weapon2;
                UIManager.instance.UpdateCurrentWeaponAmmoCount(_currentMagazineAmmo[(int)weapon], _currentBagAmmo[(int)weapon]);
            }
        }
        else if (_primaryWeapon != _gunTypes[(int)weapon])
        {
            _primaryWeapon = _gunTypes[(int)weapon];
            _weapon1 = weapon;

            if (_primaryWeaponSelected)
            {
                ShowWeapon(weapon);  //Show primary weapon
                _player.SetCurrentGun(_primaryWeapon);
                _currentWeapon = _weapon1;
                UIManager.instance.UpdateCurrentWeaponAmmoCount(_currentMagazineAmmo[(int)weapon], _currentBagAmmo[(int)weapon]);
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

    public void ExpendAmmo(Weapon weapon)
    {
        if (_currentMagazineAmmo[(int)weapon] > 0)
        {
            _ammoCarry[(int)weapon]--;
            _currentMagazineAmmo[(int)weapon]--;
            UIManager.instance.UpdateAmmoCount(weapon, _ammoCarry[(int)weapon], _currentBagAmmo[(int)weapon], _currentMagazineAmmo[(int)weapon]);
        }
    }
}
