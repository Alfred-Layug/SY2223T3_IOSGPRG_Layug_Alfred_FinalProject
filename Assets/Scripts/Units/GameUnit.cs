using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class GameUnit : MonoBehaviour
{
    [SerializeField] protected string _name;
    [SerializeField] private Health _health;
    [SerializeField] protected float _speed;
    public Gun _currentGun;

    public virtual void Initialize(string name, int maxHealth, float speed)
    {
        _name = name;
        _health = gameObject.GetComponent<Health>();
        _health.Initialize(maxHealth);
        _speed = speed;

        Debug.Log($"{_name} has been initialized");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Bullet>() != null)
        {
            Destroy(collision.gameObject);
        }
    }

    public virtual void Shoot()
    {
        Debug.Log($"Unit is shooting");
    }

    public virtual void Wander()
    {

    }

    public virtual void Seek()
    {

    }

    private void Movement()
    {

    }
}
