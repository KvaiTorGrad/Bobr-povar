using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using YG;
using YG.Example;

public class Menu : MonoBehaviour
{
    private Button _button;
    [SerializeField] private int _scaleDuration;
    [SerializeField] private GameObject _levelMenu;
    private Tween _tween;
    void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(StartGame);
        SaveGame.SaveSystem = new YandexGameSaveSystem();
        YandexGame.GetDataEvent += SaveGame.Load;
    }
    private void Start()
    {
        AnimButton();
    }

    private void StartGame()
    {
        Settings.Instance.PlayOneShotClip(0);
        if (!YandexGame.SDKEnabled) return;
        YandexGame.FullscreenShow();
        _tween.Kill();
        _levelMenu.SetActive(true);
        LevelCreator.Instance.Create();
        gameObject.SetActive(false);
    }

    private void AnimButton()
    {
        _tween = transform.DOScale(transform.localScale / 1.1f, _scaleDuration)
       .SetEase(Ease.InOutSine)
       .SetLoops(-1, LoopType.Yoyo);
    }
}
