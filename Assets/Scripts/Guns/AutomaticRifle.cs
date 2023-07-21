using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticRifle : Gun
{
    public Inventory _inventoryScript;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _nozzle;

    private void Start()
    {
        _isFiring = false;
        _stopFiring = false;
        _fireRate = 0.35f;
        _reloadTime = 2.3f;
        _bulletSpread = 1.1f;
    }

    private void Update()
    {
        if (_isFiring)
        {
            MakeIsFiringFalse();
            Shoot(_bulletPrefab, _nozzle);
        }
    }

    public override void Shoot(GameObject prefab, GameObject nozzle)
    {
        if (_inventoryScript._currentAutomaticRifleMagazineAmmo > 0)
        {
            GameObject bullet = Instantiate(prefab, nozzle.transform.position, nozzle.transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector2 dir = transform.rotation * Vector2.up;
            Vector2 perpendicularDir = Vector2.Perpendicular(dir) * Random.Range(-_bulletSpread, _bulletSpread);
            rb.velocity = (dir + perpendicularDir);
            _inventoryScript.ExpendAmmo();
            Debug.Log("Multi-Shot");
        }
    }

    public override void OnPointerDown()
    {
        base.OnPointerDown();
        _stopFiring = false;
        MakeIsFiringTrue();
    }

    public override void OnPointerUp()
    {
        base.OnPointerUp();
        _isFiring = false;
        _stopFiring = true;
    }

    private void MakeIsFiringTrue()
    {
        _isFiring = true;
    }

    private void MakeIsFiringFalse()
    {
        _isFiring = false;
        StartCoroutine(FireRateTimer());
    }

    public override IEnumerator FireRateTimer()
    {
        yield return new WaitForSeconds(_fireRate);
        if (!_stopFiring)
        {
            MakeIsFiringTrue();
        }
    }
}
