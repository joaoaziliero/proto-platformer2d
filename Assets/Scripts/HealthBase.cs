using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script que estabelece a pontua��o de vida e a condi��o de morte.

public class HealthBase : MonoBehaviour
{
    #region VARI�VEIS EXTERNAS
    public int startHealth; // Sa�de inicial.
    public bool destroyOnKill;
    #endregion

    #region VARI�VEIS PRIVADAS
    private int _currentHealth; // Sa�de atual no jogo.
    private bool _isDead;
    #endregion

    #region M�TODOS DO UNITY
    private void Start()
    {
        _currentHealth = startHealth;
        _isDead = false;

        Debug.Log("Current Health: " + _currentHealth);
    }
    #endregion

    #region M�TODOS ADICIONAIS
    // M�todo que destroi a inst�ncia de personagem
    // quando a sa�de fica nula (essa � a condi��o de morte).
    private void Kill()
    {
        _isDead = true;

        Debug.Log("Player is dead!");

        if (destroyOnKill)
        {
            gameObject.GetComponent<MotionControl>().enabled = false; // A movimenta��o � desabilitada
                                                                      // at� o momento de destrui��o.
            Destroy(gameObject, 3);
        }
    }

    // M�todo que calcula o dano recebido pelo personagem.
    public void Damage(int damage)
    {
        if (_isDead)
        {
            Debug.Log("Player is dead!");
            return;
        }
        else
        {
            _currentHealth -= damage; // C�lculo de dano, com base na sa�de atual.

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
