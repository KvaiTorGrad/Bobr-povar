using DG.Tweening;
using UnityEngine;

public class FantomAnim : AnimatDot
{
    [SerializeField] private float _scaleDuration;
    private Tween _tweenTwo;
    protected override void Start()
    {
        _startPosition = transform.position;
        SetAnimation();
    }

    public override void SetAnimation()
    {
       _tween = transform.DOMove(GameManager.Instance.BoilerUp.position + _offset, _duration)
            .SetEase(Ease.InOutSine)
            .OnStepComplete(DownAnimation);
        _tweenTwo = transform.DOScale(transform.localScale / 2, _scaleDuration * 2)
            .SetEase(Ease.InOutSine);
    }

    private void DownAnimation()
    {
      _tween = transform.DOMove(GameManager.Instance.BoilerDown.position + _offset, _duration / 4)
            .SetEase(Ease.InOutSine)
            .OnKill(DestroyObject);
        _tweenTwo = transform.DOScale(Vector3.zero, _scaleDuration)
            .SetEase(Ease.InOutSine);
    }
    private void DestroyObject()
    {
        _tween.Kill();
        _tweenTwo.Kill();
        Instantiate(GameManager.Instance.BoilerWatter,transform.position,Quaternion.identity);
        Settings.Instance.PlayOneShotClip(1);
        Destroy(gameObject);
    }
}
