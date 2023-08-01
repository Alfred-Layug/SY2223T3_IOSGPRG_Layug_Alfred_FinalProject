using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float MaxHealth
    {
        get => _maxHealth;
    }

    public float CurrentHealth
    {
        get => _currentHealth;
    }

    public Image _healthBar;
    public Image _healthBarBackground;
    public TextMeshProUGUI _healthText;
    private float _maxHealth;
    private float _currentHealth;

    public void Initialize(int maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = _maxHealth;
        _healthBar.fillAmount = _currentHealth / _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _currentHealth = Mathf.Max(_currentHealth, 0);
        _healthBar.fillAmount = _currentHealth / _maxHealth;
    }

    public void AddHealth(int amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Min(_currentHealth, _maxHealth);
        _healthBar.fillAmount = _currentHealth / _maxHealth;
    }
}
