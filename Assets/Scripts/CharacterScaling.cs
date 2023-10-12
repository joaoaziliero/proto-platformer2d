using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// Script para animação de proporções em 2D.

public class CharacterScaling : MonoBehaviour
{
    #region VARIÁVEIS
    [Header("End Values")]
    // Vetores que correspondem às proporções do personagem
    // quando pula ou quando cai.
    public Vector3 scaleOnJump;
    public Vector3 scaleOnFall;

    [Header("Durations")]
    // Durações das animações de pulo e de queda.
    public float jumpDuration;
    public float fallDuration;
    #endregion

    #region MÉTODOS
    // Método que reaplica as proporções originais do personagem.
    public void NormalizeObject()
    {
        DOTween.Kill(gameObject.transform);
        gameObject.transform.DOScale(new Vector3(1, 1, 0), 0.1f);
    }

    // Método para a animação de pulo.
    public void JumpStretch()
    {
        NormalizeObject();
        gameObject.transform.DOScale(scaleOnJump, jumpDuration)
            .SetLoops(2, LoopType.Yoyo);
    }

    // Método para a animação de queda.
    public void FallStretch()
    {
        NormalizeObject();
        gameObject.transform.DOScale(scaleOnFall, fallDuration)
            .SetLoops(2, LoopType.Yoyo);
    }
    #endregion
}
