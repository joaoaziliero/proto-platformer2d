using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// Script para anima��o de propor��es em 2D.

public class CharacterScaling : MonoBehaviour
{
    #region VARI�VEIS
    [Header("End Values")]
    // Vetores que correspondem �s propor��es do personagem
    // quando pula ou quando cai.
    public Vector3 scaleOnJump;
    public Vector3 scaleOnFall;

    [Header("Durations")]
    // Dura��es das anima��es de pulo e de queda.
    public float jumpDuration;
    public float fallDuration;
    #endregion

    #region M�TODOS
    // M�todo que reaplica as propor��es originais do personagem.
    public void NormalizeObject()
    {
        DOTween.Kill(gameObject.transform);
        gameObject.transform.DOScale(new Vector3(1, 1, 0), 0.1f);
    }

    // M�todo para a anima��o de pulo.
    public void JumpStretch()
    {
        NormalizeObject();
        gameObject.transform.DOScale(scaleOnJump, jumpDuration)
            .SetLoops(2, LoopType.Yoyo);
    }

    // M�todo para a anima��o de queda.
    public void FallStretch()
    {
        NormalizeObject();
        gameObject.transform.DOScale(scaleOnFall, fallDuration)
            .SetLoops(2, LoopType.Yoyo);
    }
    #endregion
}
