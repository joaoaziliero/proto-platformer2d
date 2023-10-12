using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script para movimenta��o em 2D.

public class MotionControl : MonoBehaviour
{
    #region REFER�NCIAS
    [Header("References")]
    // Refer�ncias para controle da movimenta��o
    // e anima��o do personagem.
    public Rigidbody2D mainCharacter;
    public CharacterScaling characterScaling;
    #endregion

    #region VARI�VEIS EXTERNAS
    [Header("Motion Settings")]
    // Configura��es de movimento:
    // velocidades, fric��o e for�a de pulo.
    public float normalSpeed;
    public float runningSpeed;
    public float frictionScale;
    public float jumpMagnitude;
    
    [Header("Jump Count")]
    // N�mero m�ximo de pulos por colis�o.
    public int maximumJumps;
    #endregion

    #region VARI�VEIS PRIVADAS
    // Vari�veis de funcionamento interno.
    private float _speedMagnitude;
    private int _jumpCount;
    private bool _notFloating;
    private Coroutine _coroutine;
    #endregion

    #region M�TODOS DO UNITY
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
        // a contagem de pulos � zerada depois de uma colis�o.
        if (mainCharacter.velocity.y <= 0)
        {
            _jumpCount = 0;
        }

        _notFloating = true;

        // Se n�o h� outras anima��es ocorrendo,
        // as dimens�es originais do personagem
        // s�o aplicadas (isto �, normalizadas).
        characterScaling.NormalizeObject();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(mainCharacter.velocity.y < 0)
        {
            _jumpCount = maximumJumps; // Este comando impede que o jogador "se cole"
                                       // � parte inferior de uma plataforma mais de uma vez.
        }
        else if (mainCharacter.velocity.y == 0)
        {
            _notFloating = true;
        }
        else
        {
            // A vari�vel _notFloating garante que o jogador
            // n�o altere sua velocidade horizontal durante um pulo.
            _notFloating = false;
        }
    }
    #endregion

    #region M�TODOS ADICIONAIS
    // M�todo de controle da velocidade horizontal baseado
    // nos efeitos das colis�es (dos m�todos acima).
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

    // M�todo de controle de pulos; tamb�m leva em conta
    // a quantidade de pulos por colis�o.
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

    // A fric��o � aplicada ao movimento horizontal do personagem,
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
    // Corrotina que regula a execu��o da anima��o quando o personagem est� caindo.
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
