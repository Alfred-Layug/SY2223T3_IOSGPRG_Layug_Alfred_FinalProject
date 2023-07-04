using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GameUnit
{
    public VariableJoystick _movementJoystick;
    [SerializeField] private GameObject _uiManager;

    private void Start()
    {
        base.Initialize("Austin", 100, 0.25f);
    }

    private void Update()
    {
        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        //{
        //    Shoot();
        //}
    }

    private void FixedUpdate()
    {
        Vector2 direction = Vector2.up * _movementJoystick.Vertical + Vector2.right * _movementJoystick.Horizontal;
        transform.Translate(direction * _speed);
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

    public void CollectAmmo(string ammoType)
    {
        UIManager uiManagerScript = _uiManager.GetComponent<UIManager>();

        if (ammoType == "Pistol Ammo")
        {
            int ammoAmmount = Random.Range(1, 9);
            uiManagerScript.UpdatePistolAmmoCount(ammoAmmount);
        }
        else if (ammoType == "Automatic Rifle Ammo")
        {
            int ammoAmmount = Random.Range(5, 16);
            uiManagerScript.UpdateAutomaticRifleAmmoCount(ammoAmmount);
        }
        else if (ammoType == "Shotgun Ammo")
        {
            int ammoAmmount = Random.Range(1, 3);
            uiManagerScript.UpdateShotgunAmmoCount(ammoAmmount);
        }
    }
}
