using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Gun
{
    public Inventory _inventoryScript;
    private bool _triggerReleased;

    private void Start()
    {
        _damage = 100;
        _canShoot = true;
        _triggerReleased = true;
        _fireRate = 5f;
        _reloadTime = 2.3f;
        _currentMagazineAmmo = 1;
    }

    public override void Shoot(GameObject prefab, GameObject nozzle)
    {
        if (_inventoryScript._currentRocketLauncherMagazineAmmo > 0 && _canShoot && _triggerReleased)
        {
            GameObject rocket = Instantiate(prefab, nozzle.transform.position, nozzle.transform.rotation);
            Rigidbody2D rb = rocket.GetComponent<Rigidbody2D>();
            Vector2 dir = transform.rotation * Vector2.up;
            rb.velocity = (dir);
            _canShoot = false;
            StartCoroutine(FireRateTimer());
            _inventoryScript.ExpendAmmo();
            Debug.Log("Rocket Fired");
        }
    }

    public override void EnemyShoot(GameObject prefab, GameObject nozzle)
    {
        if (_canShoot && _currentMagazineAmmo > 0)
        {
            GameObject rocket = Instantiate(prefab, nozzle.transform.position, nozzle.transform.rotation);
            Rigidbody2D rb = rocket.GetComponent<Rigidbody2D>();
            Vector2 dir = transform.rotation * Vector2.up;
            rb.velocity = (dir);
            _canShoot = false;
            _currentMagazineAmmo--;
            if (_currentMagazineAmmo > 0)
            {
                StartCoroutine(FireRateTimer());
            }
            else
            {
                StartCoroutine(EnemyReload());
            }
        }
    }

    public override IEnumerator FireRateTimer()
    {
        yield return new WaitForSeconds(_fireRate);
        _canShoot = true;
    }

    public override IEnumerator EnemyReload()
    {
        Debug.Log("Enemy is reloading rocket launcher");
        yield return new WaitForSeconds(_reloadTime * 2f);
        _currentMagazineAmmo = 1;
        _canShoot = true;
        Debug.Log("Enemy finished reloading rocket launcher");
    }

    public override void OnPointerDown()
    {
        base.OnPointerDown();
        _triggerReleased = false;
    }

    public override void OnPointerUp()
    {
        base.OnPointerUp();
        _triggerReleased = true;
    }
}
