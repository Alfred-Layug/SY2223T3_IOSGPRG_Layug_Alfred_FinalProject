using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    public int _damage;

    private void Start()
    {
        _speed = 15f;
    }

    private void Update()
    {
        transform.Translate(Vector2.up * _speed * Time.deltaTime);
    }

    public void SetBulletDamage(int damage)
    {
        _damage = damage;
    }
}
