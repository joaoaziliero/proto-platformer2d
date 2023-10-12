using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script para movimentação em 2D.

public class MotionControl : MonoBehaviour
{
    #region REFERÊNCIAS
    [Header("References")]
    // Referências para controle da movimentação
    // e animação do personagem.
    public Rigidbody2D mainCharacter;
    public CharacterScaling characterScaling;
    #endregion

    #region VARIÁVEIS EXTERNAS
    [Header("Motion Settings")]
    // Configurações de movimento:
    // velocidades, fricção e força de pulo.
    public float normalSpeed;
    public float runningSpeed;
    public float frictionScale;
    public float jumpMagnitude;
    
    [Header("Jump Count")]
    // Número máximo de pulos por colisão.
    public int maximumJumps;
    #endregion

    #region VARIÁVEIS PRIVADAS
    // Variáveis de funcionamento interno.
    private float _speedMagnitude;
    private int _jumpCount;
    private bool _notFloating;
    private Coroutine _coroutine;
    #endregion

    #region MÉTODOS DO UNITY
    private void Awake()
    {
        _speedMagnitude = normalSpeed;
        _jumpCount = 0;
        _notFloating = true;
        _coroutine = null;
    }

    private void Update()
    {
        HandleHorizontalMotion();
        AccountForFriction();
        HandleVerticalMotion();

        if(_coroutine == null)
        {
            _coroutine = StartCoroutine(AnimateFall());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Dependendo da velocidade vertical do personagem,
        // a contagem de pulos é zerada depois de uma colisão.
        if (mainCharacter.velocity.y <= 0)
        {
            _jumpCount = 0;
        }

        _notFloating = true;

        // Se não há outras animações ocorrendo,
        // as dimensões originais do personagem
        // são aplicadas (isto é, normalizadas).
        characterScaling.NormalizeObject();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(mainCharacter.velocity.y < 0)
        {
            _jumpCount = maximumJumps; // Este comando impede que o jogador "se cole"
                                       // à parte inferior de uma plataforma mais de uma vez.
        }
        else if (mainCharacter.velocity.y == 0)
        {
            _notFloating = true;
        }
        else
        {
            // A variável _notFloating garante que o jogador
            // não altere sua velocidade horizontal durante um pulo.
            _notFloating = false;
        }
    }
    #endregion

    #region MÉTODOS ADICIONAIS
    // Método de controle da velocidade horizontal baseado
    // nos efeitos das colisões (dos métodos acima).
    private void HandleHorizontalMotion()
    {
        if(_notFloating && _jumpCount < maximumJumps)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                _speedMagnitude = runningSpeed;
            }
            else
            {
                _speedMagnitude = normalSpeed;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                mainCharacter.velocity = new Vector2(_speedMagnitude, mainCharacter.velocity.y);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                mainCharacter.velocity = new Vector2(-_speedMagnitude, mainCharacter.velocity.y);
            }
        }
    }

    // Método de controle de pulos; também leva em conta
    // a quantidade de pulos por colisão.
    private void HandleVerticalMotion()
    {
        if(_jumpCount < maximumJumps)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                characterScaling.JumpStretch();
                mainCharacter.velocity = new Vector2(mainCharacter.velocity.x, jumpMagnitude);
                _jumpCount++;
            }
        }
    }

    // A fricção é aplicada ao movimento horizontal do personagem,
    // contanto que sua velocidade vertical seja nula.
    private void AccountForFriction()
    {
        if(mainCharacter.velocity.x != 0 && mainCharacter.velocity.y == 0)
        {
            mainCharacter.velocity -= mainCharacter.velocity.normalized * frictionScale;
        }
    }
    #endregion

    #region CORROTINAS
    // Corrotina que regula a execução da animação quando o personagem está caindo.
    IEnumerator AnimateFall()
    {
        if(mainCharacter.velocity.y < 0)
        {
            characterScaling.FallStretch();
            yield return new WaitForSeconds(characterScaling.fallDuration);
            _coroutine = null;
        }
    }
    #endregion
}
