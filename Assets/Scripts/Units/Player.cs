using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GameUnit
{
    private void Start()
    {
        base.Initialize("Moises", 100, 5);
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Shoot();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();

        if (health != null)
        {
            health.TakeDamage(5);

            Debug.Log($"{_name} dealt 5 damage to {collision.gameObject.name}");
        }
    }

    public override void Shoot()
    {
        base.Shoot();
        Debug.Log($"{_name} is shooting");
    }

    public void SetCurrentGun(Gun gun)
    {
        _currentGun = gun;
    }
}
