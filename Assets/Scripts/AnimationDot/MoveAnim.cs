using DG.Tweening;
using UnityEngine;

public class MoveAnim : AnimatDot
{
    protected override void Start()
    {
        _startPosition = transform.position;
        SetAnimation();
        if(_specialAnimObject != SpecialAnimObject.Kolpack ||
            _specialAnimObject != SpecialAnimObject.Mouth)
        _tween.Play();
    }
    public override void SetAnimation()
    {
        Vector3 targetPosition = _startPosition + _offset;

        _tween = transform.DOMove(targetPosition, _duration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo)
            .OnStepComplete(CheckCondition);
    }
}
