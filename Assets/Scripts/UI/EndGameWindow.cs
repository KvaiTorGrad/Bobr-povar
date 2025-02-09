using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

using static Cooking;

public class EndGameWindow : MonoBehaviour
{
    [SerializeField] private Image _panelEngCook;
    [SerializeField] private TextMeshProUGUI _textState, _failText;
    [SerializeField] private Transform _endWindow;
    [SerializeField] private Transform[] _stars;
    [SerializeField] private Transform _centerTarget;
    [SerializeField] private Button _reloadGame;
    private int _countStars;
    public void SetStateCook(StateDish state)
    {
        _panelEngCook.gameObject.SetActive(true);
        string text = string.Empty;
        switch (state)
        {
            case StateDish.Terrible:
                Settings.Instance.PlayOneShotClip(2);
                StartEndGame();
                break;
            case StateDish.Tasteless:
                Settings.Instance.PlayOneShotClip(2);
                StartEndGame();
                break;
            case StateDish.Normal:
                if (YandexGame.EnvironmentData.language == "en")
                    text = "Normal.";
                else if (YandexGame.EnvironmentData.language == "ru")
                    text = "Œ·˚˜Ì˚È.";
                Settings.Instance.PlayOneShotClip(3);
                _countStars = 1;
                StartEndGame(Color.grey, text);
                break;
            case StateDish.Good:
                if (YandexGame.EnvironmentData.language == "en")
                    text = "Good!";
                else if (YandexGame.EnvironmentData.language == "ru")
                    text = "¬ÍÛÒÌ˚È!";
                Settings.Instance.PlayOneShotClip(4);
                _countStars = 2;
                StartEndGame(Color.green, text);
                break;
            case StateDish.Perfect:
                if (YandexGame.EnvironmentData.language == "en")
                    text = "Perfect!";
                else if (YandexGame.EnvironmentData.language == "ru")
                    text = "Œ‘‘»√≈≈≈Õ€…!";
                Settings.Instance.PlayOneShotClip(5);
                _countStars = 3;
                StartEndGame(Color.yellow, text);
                break;
        }
    }
    private void StartEndGame(Color color, string text)
    {
        GameManager.Instance.LevelData.SetNumberStar(_countStars);
        _textState.text = text;
        StartAnimWin(color);
    }
    private void StartEndGame()
    {
        _panelEngCook.DOFade(0.5f, 1)
            .SetEase(Ease.Linear);
        ReloadAnim();
    }
    private void ReloadAnim()
    {
        _failText.gameObject.SetActive(true);
        _failText.transform.localScale = Vector3.zero;
        _failText.transform.DOScale(Vector3.one, 1)
            .SetEase(Ease.Linear);
        _failText.transform.DORotate(new Vector3(0f, 0f, 360), 1, RotateMode.FastBeyond360)
            .SetEase(Ease.InOutSine);
        _reloadGame.GetComponent<Image>().DOFade(1, 0.5f)
            .SetDelay(0.8f)
            .SetEase(Ease.Linear);
        _reloadGame.transform.GetChild(0).GetComponent<Image>().DOFade(1, 0.5f)
            .SetDelay(0.8f)
            .SetEase(Ease.Linear);
    }

    private void StartAnimWin(Color color)
    {
        _panelEngCook.DOFade(0.5f, 1)
            .SetEase(Ease.Linear);
        ShowWindow(color);
    }
    private void ShowWindow(Color color)
    {
        _endWindow.DOScale(Vector3.one, 1f)
            .SetEase(Ease.Linear)
            .OnKill(ShowStar);
        _textState.DOColor(color, 1)
            .SetLoops(-1, LoopType.Restart);
    }

    private void ShowStar()
    {
        for (int i = 0; i < _countStars; i++)
        {
            _stars[i].gameObject.SetActive(true);
            _stars[i].transform.DOScale(Vector3.one, 0.3f)
                .SetEase(Ease.Linear);
        }
    }
}
