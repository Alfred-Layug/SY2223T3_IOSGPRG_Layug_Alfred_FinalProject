using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] protected int _damage;
    [SerializeField] private float _spread;
    public int _currentMagazineAmmo;
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

    public virtual void EnemyShoot(GameObject prefab, GameObject nozzle)
    {

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

    public virtual IEnumerator EnemyReload()
    {
        yield return new WaitForSeconds(_reloadTime);
    }
}
