using System;
using UnityEngine;
using YG.Example;
using static Cooking;
[CreateAssetMenu(fileName = "LevelData", menuName = "LevelData")]
public class LevelDataSO : ScriptableObject
{
    [Serializable]
    public class LevelData
    {
        public bool isOpen;
        public int numberStars;
        public bool isShowSign;
        public int numberStarsForOpenLevel;
        public LevelSetting levelSetting;
        public Recipes[] recipes;
    }

    [Serializable]
    public struct LevelSetting
    {
        public int[] ingradientIndexList;
        public int[] badIngradients;
    }
    [Serializable]
    public struct Recipes
    {
        public string recipes;
        public StateDish stateDish;
    }

    public LevelData[] Levels;
    [SerializeField] private GameObject _prefabLevel;
    [SerializeField] private int _activeLevel;
    [SerializeField] private int _allStars;
    public GameObject PrefabLevel => _prefabLevel;
    public int AllStars { get => _allStars; set => _allStars = value; }
    public int ActiveLevel { get => _activeLevel; set => _activeLevel = value; }

    public void SetActiveLevel(int level) => _activeLevel = level;

    public void SetNumberStar(int newStars)
    {
        if (Levels[ActiveLevel].numberStars > newStars) return;
        Levels[ActiveLevel].numberStars = newStars;
        SetAllStart();
        InitNextLevel();
        SaveGame.Save();
    }
    private void InitNextLevel()
    {
        Levels[ActiveLevel].isShowSign = false;
        if (ActiveLevel + 1 > Levels.Length - 1) return;
        var nextLevel = Levels[ActiveLevel + 1];
        if (nextLevel.numberStars == 0)
            nextLevel.isShowSign = true;
        if (AllStars >= nextLevel.numberStarsForOpenLevel)
            nextLevel.isOpen = true;
    }

    private void SetAllStart()
    {
        AllStars = 0;
        foreach (var level in Levels)
            AllStars += level.numberStars;
    }

    public void ResetData()
    {
        AllStars = 0;
        for (int i = 0; i < Levels.Length; i++)
        {
            var level = Levels[i];
            level.numberStars = 0;
            if (i == 0)
            {
                level.isOpen = true;
                level.isShowSign = true;
                continue;
            }
            level.isOpen = false;
            level.isShowSign = false;
        }
    }
}
