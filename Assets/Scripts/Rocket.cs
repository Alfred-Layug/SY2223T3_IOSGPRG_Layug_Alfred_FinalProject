using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionParticleSystem;
    [SerializeField] private float _speed;
    public int _damage;
    private Rigidbody2D _rb2d;
    private SpriteRenderer _spriteRenderer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<GameUnit>() != null ||
            collision.gameObject.GetComponent<Obstacle>() != null)
        {
            _speed = 0;
            _rb2d.velocity = Vector2.zero;
            _explosionParticleSystem.Play();
            _spriteRenderer.enabled = false;
            this.GetComponent<BoxCollider2D>().enabled = false;
            this.GetComponent<CircleCollider2D>().enabled = true;
            Destroy(gameObject, 1f);
        }

        if (collision.gameObject.GetComponent<Health>() != null)
        {
            Health healthScript = collision.gameObject.GetComponent<Health>();
            GameUnit gameUnitScript = collision.gameObject.GetComponent<GameUnit>();
            healthScript.TakeDamage(_damage);
            gameUnitScript.StartCoroutine("ShowEnemyHealthBar");
            if (healthScript.CurrentHealth <= 0)
            {
                gameUnitScript.DoDeath();
            }
        }
    }

    private void Start()
    {
        _speed = 15f;
        _damage = 100;
        this.GetComponent<CircleCollider2D>().enabled = false;
        _rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        _spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.Translate(Vector2.up * _speed * Time.deltaTime);
    }
}
