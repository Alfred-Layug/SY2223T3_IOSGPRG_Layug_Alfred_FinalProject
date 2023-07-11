using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] protected Weapon _weapon;

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
            playerInventory.LootWeapon(_weapon);
            Spawner.instance.RemoveWeaponPickupFromList(this);
            Destroy(gameObject);
        }
    }
}
