using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] protected string _ammoType;

    public virtual void Initialize(string ammoType)
    {
        _ammoType = ammoType;

        Debug.Log($"{_ammoType} has been initialized");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            Player playerScript = collision.gameObject.GetComponent<Player>();
            playerScript.CollectAmmo(_ammoType);
            Destroy(gameObject);
        }
    }
}
