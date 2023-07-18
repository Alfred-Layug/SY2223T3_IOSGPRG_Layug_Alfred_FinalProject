using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _spread;
    public float _fireRate;
    public float _reloadTime;
    public float _bulletSpread;

    private int _currentAmmo;
    public bool _canShoot;

    public virtual void Shoot(GameObject prefab, GameObject nozzle)
    {
        Debug.Log("Base Gun Shooting");
    }

    private void Reload()
    {

    }
}
