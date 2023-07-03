using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private int _pistolAmmoMaxCarry;

    private int _pistolAmmoCarry;
    private int _healthKits;

    private Gun _primaryWeapon;
    private Gun _secondaryWeapon;

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

    public void AddAmmo(Weapon weapon, int amount)
    {
        if (weapon == Weapon.Pistol)
        {
            _pistolAmmoCarry += amount;

            _pistolAmmoCarry = Mathf.Min(_pistolAmmoCarry, _pistolAmmoMaxCarry);
        }
    }
}
