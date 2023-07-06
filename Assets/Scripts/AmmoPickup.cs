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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Inventory>() != null)
        {
            Inventory playerInventory = collision.gameObject.GetComponent<Inventory>();
            playerInventory.AddAmmo(_ammoType);
            Destroy(gameObject);
        }
    }
}
