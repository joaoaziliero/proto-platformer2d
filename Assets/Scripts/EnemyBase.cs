using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script que trata do dano ao personagem na colisão com um inimigo.

public class EnemyBase : MonoBehaviour
{
    #region VARIÁVEIS
    public int damage; // Dano a ser subtraído da saúde atual no jogo.
    #endregion

    #region MÉTODOS
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var health = collision.gameObject.GetComponent<HealthBase>();

        // Se uma classe do tibo HealthBase estiver associada
        // ao objeto colisor, a subtração é calculada.
        if(health != null)
        {
            health.Damage(damage);
        }
    }
    #endregion
}
