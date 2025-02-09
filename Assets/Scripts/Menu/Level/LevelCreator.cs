using DG.Tweening;
using TMPro;
using UnityEngine;

public class LevelCreator : SingleTon.SingletonBase<LevelCreator>
{
    [SerializeField] private LevelDataSO _levelData;
    [SerializeField] private Transform _allStarIcon;
    [SerializeField] private TextMeshProUGUI _allStarText;
    [SerializeField] private Transform _parentForLevelUI;
    public LevelDataSO LevelData { get => _levelData; set => _levelData = value; }

    public void Create()
    {
        CreateButton();
        ShowAllStar();
    }
    private void CreateButton()
    {
        for (int i = 0; i < _levelData.Levels.Length; i++)
        {
            var data = _levelData.Levels[i];
            var newLevel = Instantiate(_levelData.PrefabLevel, _parentForLevelUI).GetComponent<Level>();
            newLevel.SetNumberLevel(i + 1);
            newLevel.InitGetStar(data.numberStars);
            newLevel.UnLockAndSignLevel(data.isOpen, data.isShowSign);
            newLevel.SetStarForOpen(data.numberStarsForOpenLevel, LevelData.AllStars);
        }
    }

    private void ShowAllStar()
    {
        _allStarText.text = _levelData.AllStars.ToString();
        _allStarIcon.transform.DOScale(_allStarIcon.transform.localScale / 1.2f, 1)
             .SetEase(Ease.Linear)
             .SetLoops(-1, LoopType.Yoyo);
    }
}
