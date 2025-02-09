using DG.Tweening;
using SingleTon;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using YG;

public class Speak : SingletonBase<Speak>
{
    [SerializeField] private CloudAnim _cloudAnim;
    [SerializeField] private TextMeshProUGUI _textUI;
    [SerializeField] private Tween _tween;
    [SerializeField] private float _duration;
    private string _textsToDisplay;
    private bool _isProccesingTimer, _isProccesingSpeak, _isSpeak;
    private int _remainingTime;
    private Action OpenCloud;

    protected override void Awake()
    {
        base.Awake();
        OpenCloud += SpeakBegine;
    }

    private void Start()
    {
        string startText = string.Empty;
        if (YandexGame.EnvironmentData.language == "ru")
            startText = "Приготовим полено? На столе ингредиенты, добавляй их на свое усмотрение. Аккуратнее, что не все инградиенты сочетаются!";
        else if (YandexGame.EnvironmentData.language == "en")
            startText = "Shall we cook a log? There are ingredients on the table, add them at your discretion. Be careful that not all ingredients are combined!";
        SpawnClouds(startText);
    }

    public void SpawnClouds(string text)
    {
        _textsToDisplay = text;
        if (!_cloudAnim.IsOpenCloud)
            StartCoroutine(_cloudAnim.OpenCloud(OpenCloud));
        else
            OpenCloud.Invoke();
    }
    private void SpeakBegine()
    {
        _tween?.Kill();
        _textUI.text = string.Empty;
        _tween = _textUI.DOText(_textsToDisplay, _duration * _textsToDisplay.Length)
            .SetEase(Ease.Linear)
            .OnKill(delegate { StartCoroutine(Timer()); });
        _isSpeak = true;
        StartCoroutine("SpeakAudioPlay");
    }
    private IEnumerator SpeakAudioPlay()
    {
        if (!_isProccesingSpeak)
        {
            _isProccesingSpeak = true;
            while (_isSpeak)
            {
                yield return new WaitForSeconds(0.1f);
                Settings.Instance.PlayOneShotClip(6);
            }
            _isProccesingSpeak = false;
        }
    }
    private IEnumerator Timer()
    {
        _remainingTime = 3;
        if (!_isProccesingTimer)
        {
            _isProccesingTimer = true;
            while (_remainingTime != 0)
            {
                yield return new WaitForSeconds(1);
                _remainingTime--;
            }
            EndSpeak();
            _isProccesingTimer = false;
        }
    }
    private void EndSpeak()
    {
        StartCoroutine(_cloudAnim.CloseCloud());
    }

    public void ResetParametrsSpeak()
    {
        _textsToDisplay = string.Empty;
        _textUI.text = string.Empty;
        _isSpeak = false;
    }

    private void OnDestroy()
    {
        OpenCloud -= SpeakBegine;
        _tween?.Kill();
    }
}
