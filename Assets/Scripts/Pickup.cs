using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] protected Weapon _weapon;
    public bool _isWeaponPickup;

    public virtual void Initialize(Weapon weapon)
    {
        _weapon = weapon;
        Debug.Log($"{_weapon} has been initialized");
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
                playerInventory.AddAmmo(_weapon);
            }
            Spawner.instance.RemovePickupFromList(this);
            Destroy(gameObject);
        }
    }
}
