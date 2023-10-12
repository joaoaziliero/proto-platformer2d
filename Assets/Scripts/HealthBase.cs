using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public int startHealth;
    public bool destroyOnKill;

    private int _currentHealth;
    private bool _isDead;

    private void Start()
    {
        _currentHealth = startHealth;
        _isDead = false;
    }

    private void Kill()
    {
        _isDead = true;

        Debug.Log("Player is dead!");

        if (destroyOnKill)
        {
            Destroy(gameObject, 1);
        }
    }

    public void Damage(int damage)
    {
        if (_isDead)
        {
            Debug.Log("Player is dead!");
            return;
        }
        else
        {
            _currentHealth -= damage;

            if(_currentHealth <= 0)
            {
                Kill();
            }
            else
            {
                Debug.Log("Current Health: " + _currentHealth);
            }
        }
    }
}
