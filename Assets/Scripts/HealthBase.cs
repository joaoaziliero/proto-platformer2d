using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script que estabelece a pontuação de vida e a condição de morte.

public class HealthBase : MonoBehaviour
{
    #region VARIÁVEIS EXTERNAS
    public int startHealth; // Saúde inicial.
    public bool destroyOnKill;
    #endregion

    #region VARIÁVEIS PRIVADAS
    private int _currentHealth; // Saúde atual no jogo.
    private bool _isDead;
    #endregion

    #region MÉTODOS DO UNITY
    private void Start()
    {
        _currentHealth = startHealth;
        _isDead = false;

        Debug.Log("Current Health: " + _currentHealth);
    }
    #endregion

    #region MÉTODOS ADICIONAIS
    // Método que destroi a instância de personagem
    // quando a saúde fica nula (essa é a condição de morte).
    private void Kill()
    {
        _isDead = true;

        Debug.Log("Player is dead!");

        if (destroyOnKill)
        {
            gameObject.GetComponent<MotionControl>().enabled = false; // A movimentação é desabilitada
                                                                      // até o momento de destruição.
            Destroy(gameObject, 3);
        }
    }

    // Método que calcula o dano recebido pelo personagem.
    public void Damage(int damage)
    {
        if (_isDead)
        {
            Debug.Log("Player is dead!");
            return;
        }
        else
        {
            _currentHealth -= damage; // Cálculo de dano, com base na saúde atual.

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
    #endregion
}
