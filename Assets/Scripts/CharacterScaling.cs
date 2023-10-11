using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterScaling : MonoBehaviour
{
    [Header("End Values")]
    [SerializeField] public Vector3 scaleOnJump;
    [SerializeField] public Vector3 scaleOnFall;

    [Header("Durations")]
    [SerializeField] public float jumpDuration;
    [SerializeField] public float fallDuration;

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

    public void FallStretch()
    {
        NormalizeObject();
        gameObject.transform.DOScale(scaleOnFall, fallDuration)
            .SetLoops(2, LoopType.Yoyo);
    }
}
