using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] protected Weapon _weapon;
    public bool _isWeaponPickup;
    private int _ammoAmount;

    public virtual void Initialize(Weapon weapon)
    {
        _weapon = weapon;
        Debug.Log($"{_weapon} has been initialized");

        if (_weapon == Weapon.Pistol)
        {
            _ammoAmount = Random.Range(1, 9);
        }
        else if (_weapon == Weapon.AutomaticRifle)
        {
            _ammoAmount = Random.Range(5, 16);
        }
        else if (_weapon == Weapon.Shotgun)
        {
            _ammoAmount = Random.Range(1, 3);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Inventory>() != null)
        {
            Inventory playerInventory = collision.GetComponent<Inventory>();
            if (_isWeaponPickup)
            {
                playerInventory.LootWeapon(_weapon);
            }
            else
            {
                playerInventory.AddAmmo(_weapon, _ammoAmount);
            }
            Spawner.instance.RemovePickupFromList(this);
            Destroy(gameObject);
        }
    }
}
