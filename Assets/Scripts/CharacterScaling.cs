using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterScaling : MonoBehaviour
{
    [Header("End Values")]
    [SerializeField] public Vector3 scaleOnJump;

    [Header("Durations")]
    [SerializeField] public float jumpDuration;

    public void NormalizeObject()
    {
        DOTween.Kill(gameObject.transform);
        gameObject.transform.DOScale(new Vector3(1, 1, 0), 0.1f);
    }
    public void JumpStretch()
    {
        NormalizeObject();
        gameObject.transform.DOScale(scaleOnJump, jumpDuration)
            .SetLoops(2, LoopType.Yoyo);
    }
}
