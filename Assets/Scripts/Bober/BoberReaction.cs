using DG.Tweening;
using SingleTon;
using System.Collections;
using UnityEngine;
using static Cooking;

public class BoberReaction : SingletonBase<BoberReaction>
{
    [SerializeField] private AnimatDot _hand, _kolpak, _tail, _mouth;
    [SerializeField] private SpriteRenderer _headMask, _eues, _head;
    [SerializeField] private Sprite[] _euesSprites;
    [SerializeField] private Transform _targetWalk;
    public AnimatDot Mouth => _mouth;
    public void SetReact(StateDish stateDish)
    {
        switch (stateDish)
        {
            case StateDish.Terrible:
                TerribleAnim(stateDish);
                break;
            case StateDish.Tasteless:
                TastelessAnim(stateDish);
                break;
            case StateDish.Normal:
                NormalAnim(stateDish);
                break;
            case StateDish.Good:
                GoodAnim(stateDish);
                break;
            case StateDish.Perfect:
                PerfectAnim(stateDish);
                break;
        }
    }

    public void ProbaAnima()
    {
        _hand.Tween.Pause();
        _kolpak.Tween.Pause();
        _mouth.Tween.Pause();
        _tail.Tween.Pause();

        _hand.transform.DOMove(_hand.transform.position + new Vector3(-1.2f, -0.8f), 0.3f)
            .SetEase(Ease.InOutSine)
            .OnKill(delegate { StartCoroutine(Expectation.ExpectationTimer(1, delegate { GameManager.Instance.Cooking.DistributionIngradient(); })); });
        _hand.transform.DORotate(_hand.transform.position + new Vector3(0, 180, -65), 0.2f)
            .SetEase(Ease.InOutSine);
    }
    private void TerribleAnim(StateDish stateDish)
    {
        _eues.sprite = _euesSprites[0];
        _headMask.DOFade(0.3f, 1)
            .SetEase(Ease.Linear)
            .OnKill(delegate { RotateBobr(stateDish); });
    }
    private void RotateBobr(StateDish stateDish)
    {
        transform.DORotate(transform.position + new Vector3(0, 0, 90), 0.35f)
        .SetEase(Ease.InOutSine);
        StartCoroutine(Expectation.ExpectationTimer(1, delegate { OpenEndWindow(stateDish); }));
    }

    private void TastelessAnim(StateDish stateDish)
    {
        _eues.sprite = _euesSprites[1];
        transform.DOMoveX(_targetWalk.TransformDirection(_targetWalk.position).x, 4)
            .SetEase(Ease.Linear);
        transform.DORotate(new Vector3(0f, 0f, 10f), 0.3f, RotateMode.FastBeyond360)
                   .SetLoops(-1, LoopType.Yoyo)
                   .SetEase(Ease.InOutSine);
        StartCoroutine(Expectation.ExpectationTimer(1, delegate { OpenEndWindow(stateDish); }));
    }

    private void NormalAnim(StateDish stateDish)
    {
        _head.transform.DORotate(new Vector3(0f, 40f, 0), 0.3f, RotateMode.FastBeyond360)
                   .SetLoops(-1, LoopType.Yoyo)
                   .SetEase(Ease.InOutSine);
        StartCoroutine(Expectation.ExpectationTimer(1, delegate { OpenEndWindow(stateDish); }));
    }
    private void GoodAnim(StateDish stateDish)
    {
        _eues.sprite = _euesSprites[2];
        _hand.transform.DOMove(_hand.transform.position + new Vector3(1.1f, 0.85f), 0.3f)
                   .SetLoops(-1, LoopType.Yoyo)
                   .SetEase(Ease.Linear);
        _hand.transform.DORotate(new Vector3(0f, 20f, 0), 0.3f, RotateMode.FastBeyond360)
                   .SetLoops(-1, LoopType.Yoyo)
                   .SetEase(Ease.InOutSine);
        StartCoroutine(Expectation.ExpectationTimer(1, delegate { OpenEndWindow(stateDish); }));
    }

    private void PerfectAnim(StateDish stateDish)
    {
        _eues.sprite = _euesSprites[3];
        _kolpak.transform.DOJump(_kolpak.transform.position + new Vector3(0, 1, 0), 2, 5, 1f)
                   .SetLoops(-1, LoopType.Yoyo)
                   .SetEase(Ease.Linear);
        StartCoroutine(Expectation.ExpectationTimer(1,delegate { OpenEndWindow(stateDish); }));
    }

    private void OpenEndWindow(StateDish stateDish)
    {
        GameManager.Instance.EndGameWindow.SetStateCook(stateDish);
    }
}
