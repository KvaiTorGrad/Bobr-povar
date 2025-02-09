using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public struct Cooking
{
    public enum StateDish
    {
        Terrible,
        Tasteless,
        Normal,
        Good,
        Perfect
    }

    private int _indexCount;
    private Dictionary<int, int> _interactbleIngredients;
    private Dictionary<int, List<int>> _countIngradients;
    private StringBuilder _ingradientAllList;

    public void AddIngradient(int ingredient)
    {
        _interactbleIngredients ??= new();
        _interactbleIngredients.Add(_indexCount, ingredient);
        _indexCount++;
    }

    public readonly void EndCooking(out bool _isEndCook)
    {
        _isEndCook = false;
        if (_interactbleIngredients == null) return;
        _isEndCook = true;
        CursorInteractble.DisableInput();
        CursorInteractble.DestroyInput();
        BoberReaction.Instance.ProbaAnima();
    }

    public void DistributionIngradient()
    {
        _countIngradients = new();
        _ingradientAllList = new();
        var parametrs = GameManager.Instance.IngradientsData.Parametrs;
        foreach (var ingradients in _interactbleIngredients.Values)
        {
            foreach (var index in parametrs)
            {
                if (!_countIngradients.ContainsKey(ingradients))
                    _countIngradients.Add(ingradients, new List<int>());
                if (index.index == ingradients)
                {
                    _countIngradients.TryGetValue(ingradients, out List<int> value);
                    value.Add(ingradients);
                }
            }
        }
        InteractbleBober();
    }
    private readonly void InteractbleBober() => BoberReaction.Instance.SetReact(CheckingConditionDish());

    private readonly StateDish CheckingConditionDish()
    {
        List<int> failIngradients = new();
        var data = GameManager.Instance.LevelData;
        var badIngradients = data.Levels[data.ActiveLevel].levelSetting.badIngradients;
        var sortDictionary = _countIngradients.OrderBy(kvp => kvp.Key).ToList();
        foreach (var IndexIngradients in sortDictionary)
        {
            for (int i = 0; i < IndexIngradients.Value.Count; i++)
            {
                var value = IndexIngradients.Value;
                if (badIngradients.Length != 0)
                {
                    foreach (var bad in badIngradients)
                    {
                        if (value.Count > 4)
                        {
                            if (value[i] != bad)
                                return StateDish.Tasteless;
                            else if (value[i] == bad)
                                return StateDish.Terrible;
                        }
                        else if (value[i] == bad)
                        {
                            failIngradients.Add(value[i]);
                            break;
                        }
                        else
                        {
                            _ingradientAllList.Append(value[i]);
                            break;
                        }
                    }
                }
                else
                {
                    _ingradientAllList.Append(value[i]);
                    break;
                }
            }
        }

        if (failIngradients.Count is not 0 and < 2)
            return StateDish.Tasteless;
        else if (failIngradients.Count is not 0 and >= 2)
            return StateDish.Terrible;
        foreach (var recipe in data.Levels[data.ActiveLevel].recipes)
        {
            if (recipe.recipes == _ingradientAllList.ToString())
                return recipe.stateDish;
        }
        return StateDish.Normal;
    }
}
