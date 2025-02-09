using DG.Tweening;
using UnityEngine;

public class RotateAnim : AnimatDot
{
    protected override void Start()
    {
        _startPosition = transform.eulerAngles;
        SetAnimation();
        if (_specialAnimObject != SpecialAnimObject.Kolpack)
            _tween.Play();
    }

    public override void SetAnimation()
    {
        Vector3 targetRotation = _startPosition + _offset;
        _tween = transform.DORotate(targetRotation, _duration)
            .SetEase(Ease.InOutSine)
    .SetLoops(-1, LoopType.Yoyo)
    .OnStepComplete(CheckCondition);
    }

}
