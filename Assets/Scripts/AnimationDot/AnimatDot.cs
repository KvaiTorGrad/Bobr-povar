using DG.Tweening;
using UnityEngine;

public abstract class AnimatDot : MonoBehaviour
{
    protected enum SpecialAnimObject
    {
        BobrLeftHand,
        Kolpack,
        Mouth,
        Tail,
        Log,
        Other
    }
    [SerializeField] protected SpecialAnimObject _specialAnimObject;
    [SerializeField] protected Vector3 _offset;
    [SerializeField] protected float _duration;
    public bool isStop;
    protected Vector3 _startPosition;
    protected Tweener _tween;
    public Tween Tween => _tween;

    protected abstract void Start();

    public abstract void SetAnimation();

    protected void CheckCondition()
    {
        if (!isStop) return;
        _tween.Kill();
    }
    protected virtual void OnDestroy()
    {
        _tween.Kill();
    }

}
