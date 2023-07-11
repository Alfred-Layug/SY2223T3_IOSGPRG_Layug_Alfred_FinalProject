using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] protected AmmoType _ammoType;

    public virtual void Initialize(AmmoType ammoType)
    {
        _ammoType = ammoType;

        Debug.Log($"{_ammoType} has been initialized");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Inventory>() != null)
        {
            Inventory playerInventory = collision.GetComponent<Inventory>();
            playerInventory.AddAmmo(_ammoType);
            Spawner.instance.RemoveAmmoPickupFromList(this);
            Destroy(gameObject);
        }
    }
}