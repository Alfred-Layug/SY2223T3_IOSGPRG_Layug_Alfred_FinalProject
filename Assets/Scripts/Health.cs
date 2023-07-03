using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int MaxHealth
    {
        get => _maxHealth;
    }

    public int CurrentHealth
    {
        get => _currentHealth;
    }

    private int _maxHealth;
    private int _currentHealth;

    public void Initialize(int maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        _currentHealth = Mathf.Max(_currentHealth, 0);
    }

    public void AddHealth(int amount)
    {
        _currentHealth += amount;

        _currentHealth = Mathf.Min(_currentHealth, _maxHealth);
    }
}
