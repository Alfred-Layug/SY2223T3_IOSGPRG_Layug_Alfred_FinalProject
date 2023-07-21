using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _spread;
    private int _currentAmmo;
    public float _fireRate;
    public float _reloadTime;
    public float _bulletSpread;
    public bool _canShoot;
    public bool _isFiring;
    public bool _stopFiring;

    public virtual void Shoot(GameObject prefab, GameObject nozzle)
    {
        Debug.Log("Base Gun Shooting");
    }

    public virtual IEnumerator FireRateTimer()
    {
        yield return new WaitForSeconds(_fireRate);
    }

    public virtual void OnPointerDown()
    {

    }

    public virtual void OnPointerUp()
    {

    }

    private void Reload()
    {

    }
}
