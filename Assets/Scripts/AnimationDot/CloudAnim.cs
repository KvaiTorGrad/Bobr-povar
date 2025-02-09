using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class CloudAnim : AnimatDot
{
    [SerializeField] private float _scaleDuration;
    public bool IsOpenCloud => isStop;
    private bool _isProccesing;

    protected override void Start()
    {
        _startPosition = transform.position;
    }
    public IEnumerator OpenCloud(Action isOpenCloud)
    {
        if (!_isProccesing)
        {
            _isProccesing = true;
            SetAnimation();
            while (!isStop)
                yield return null;
            BoberReaction.Instance.Mouth.Tween.Play();
            isOpenCloud.Invoke();
            _isProccesing = false;
        }
    }
    public IEnumerator CloseCloud()
    {
        if (!_isProccesing)
        {
            Speak.Instance.ResetParametrsSpeak();
            BoberReaction.Instance.Mouth.Tween.Pause();
            _isProccesing = true;
            _tween = transform.DOMove(_startPosition, _duration)
            .SetEase(Ease.InOutSine)
            .OnKill(Close);
            transform.DOScale(Vector2.zero, _scaleDuration)
                .SetEase(Ease.InOutSine);
            while (isStop)
                yield return null;
            _tween.Kill();
            _isProccesing = false;
        }
    }
    public override void SetAnimation()
    {
        _tween = transform.DOMove(_startPosition + _offset, _duration)
            .SetEase(Ease.InOutSine)
            .OnKill(Open);
        transform.DOScale(Vector2.one, _scaleDuration)
            .SetEase(Ease.InOutSine);
    }
    private void Open() => isStop = true;
    private void Close() => isStop = false;
}
