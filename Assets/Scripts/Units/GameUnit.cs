using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class GameUnit : MonoBehaviour
{
    [SerializeField] protected string _name;
    [SerializeField] protected Health _health;
    [SerializeField] protected float _speed;
    [SerializeField] private Rigidbody2D _rb2d;
    public Gun _currentGun;

    public virtual void Initialize(string name, int maxHealth, float speed)
    {
        _name = name;
        _health = gameObject.GetComponent<Health>();
        _health.Initialize(maxHealth);
        _rb2d = gameObject.GetComponent<Rigidbody2D>();
        _speed = speed;
        Debug.Log($"{_name} has been initialized");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Bullet>() != null)
        {
            Bullet bulletScript = collision.gameObject.GetComponent<Bullet>();
            _health.TakeDamage(bulletScript._damage);
            StartCoroutine(ShowEnemyHealthBar());
            if (_health.CurrentHealth <= 0)
            {
                DoDeath();
            }
            Destroy(collision.gameObject);
            _rb2d.velocity = Vector2.zero;
        }
    }

    public virtual void Shoot()
    {
        Debug.Log($"Unit is shooting");
    }

    public virtual float GetSpeed()
    {
        return _speed;
    }

    public virtual void DoDeath()
    {

    }

    public virtual IEnumerator ShowEnemyHealthBar()
    {
        yield return new WaitForSeconds(5);
    }
}
