using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    private Button _button;
    [SerializeField] private Image[] _stars;
    [SerializeField] private GameObject _lock, _sign, _starForOpenLvlIcon;
    [SerializeField] private TextMeshProUGUI _textLevel, _starForOpenLvlText;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(StartLevel);
    }

    public void InitGetStar(int countStars)
    {
        for (int i = 0; i < countStars; i++)
            _stars[i].color = Color.white;
    }

    public void SetNumberLevel(int numberLevel)
    {
        _textLevel.text = numberLevel.ToString();
    }

    public void UnLockAndSignLevel(bool unlock, bool isSign)
    {
        if (unlock)
        {
            _lock.SetActive(false);
            _button.interactable = true;
        }
        if (isSign)
        {
            _sign.SetActive(true);
            StartAnimSign();
        }
    }

    public void SetStarForOpen(int startForOpen, int allStars)
    {
        if (startForOpen == 0 || allStars >= startForOpen) return;
        _starForOpenLvlIcon.SetActive(true);
        _starForOpenLvlText.text = startForOpen.ToString();
        _starForOpenLvlIcon.transform.DOScale(_starForOpenLvlIcon.transform.localScale / 1.2f, 1)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void StartAnimSign()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_sign.transform.DORotate(new Vector3(0, 0, 45), 1f, RotateMode.FastBeyond360));
        sequence.Append(_sign.transform.DORotate(new Vector3(0, 0, -45), 1f, RotateMode.FastBeyond360));
        sequence.Append(_sign.transform.DORotate(new Vector3(0, 0, 0), 1f, RotateMode.FastBeyond360));
        sequence.AppendInterval(1f);
        sequence.SetLoops(-1, LoopType.Yoyo);
    }

    private void StartLevel()
    {
        LevelCreator.Instance.LevelData.ActiveLevel = int.Parse(_textLevel.text) - 1;
        SceneManager.LoadScene(1);
    }
}
