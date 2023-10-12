using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script que trata do dano ao personagem na colis�o com um inimigo.

public class EnemyBase : MonoBehaviour
{
    #region VARI�VEIS
    public int damage; // Dano a ser subtra�do da sa�de atual no jogo.
    #endregion

    #region M�TODOS
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var health = collision.gameObject.GetComponent<HealthBase>();

        // Se uma classe do tibo HealthBase estiver associada
        // ao objeto colisor, a subtra��o � calculada.
        if(health != null)
        {
            health.Damage(damage);
        }
    }
    #endregion
}
