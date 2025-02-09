using SingleTon;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class GameManager : SingletonBase<GameManager>
{
    private Cooking _cooking;
    public Cooking Cooking => _cooking;
    [SerializeField] private IngradientsData _data;
    [SerializeField] private LevelDataSO _levelData;
    [SerializeField] private Transform _boilerUp, _boilerDown;
    [SerializeField] private ParticleSystem _boilerWatter;
    [SerializeField] private EndGameWindow _endGameWindow;
    [SerializeField] private SpriteRenderer[] _ingradientsInGame;
    [SerializeField] private CloudAnim _cloudAnim;
    public Transform BoilerUp => _boilerUp;
    public Transform BoilerDown => _boilerDown;
    public ParticleSystem BoilerWatter => _boilerWatter;
    public IngradientsData IngradientsData => _data;
    public LevelDataSO LevelData => _levelData;
    public EndGameWindow EndGameWindow => _endGameWindow;

    protected override void Awake()
    {
        base.Awake();
        CursorInteractble.EnableInput();
    }
    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        for (int i = 0; i < _ingradientsInGame.Length; i++)
        {
            foreach (var ingradientLevel in _levelData.Levels[_levelData.ActiveLevel].levelSetting.ingradientIndexList)
            {
                if (i == ingradientLevel)
                {
                    _ingradientsInGame[i].gameObject.SetActive(true);
                    break;
                }
            }
        }
    }
    public void GetIngradient(IngradientIDName ingradientIDName, out GameObject ingradientAction)
    {
        foreach (var ingradient in IngradientsData.Parametrs)
        {
            if (ingradientIDName == ingradient.ingradientID)
            {
                _cooking.AddIngradient(ingradient.index);
                ingradientAction = ingradient.fantom;
                Speak.Instance.SpawnClouds(Replicas.SetReplicas(ingradient));
                return;
            }
        }
        ingradientAction = null;
    }
    public void EndCooking(out bool _isEndCook)
    {
        Cooking.EndCooking(out bool _endCook);
        _isEndCook = _endCook;
        StartCoroutine(_cloudAnim.CloseCloud());
    }

    public void ReloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    public void ExitGame()
    {
        CursorInteractble.DisableInput();
        CursorInteractble.DestroyInput();
        SceneManager.LoadScene(0);
    }
}

public static class Replicas
{
    private readonly static string[] _replicasRu = new string[]
    {
        "Решил добавить ",
        "Ты кинул ",
        "Интересное решение, добавить ",
        "Может ненадо добавлять ",
        "К бревну добавил "
    };

    private readonly static string[] _replicasEng = new string[]
    {
        "I decided to add ",
        "You threw ",
        "An interesting solution, add ",
        "Maybe you don't need to add ",
        "Added to the log "
    };

    public static string SetReplicas(IngradientsData.IngradientParametrs ingradientIDName)
    {
        if (YandexGame.EnvironmentData.language == "ru")
        {
            int randomIndex = Random.Range(0, _replicasRu.Length);
            return _replicasRu[randomIndex] + ingradientIDName.nameRU;
        }
        else if (YandexGame.EnvironmentData.language == "en")
        {
            int randomIndex = Random.Range(0, _replicasRu.Length);
            return _replicasEng[randomIndex] + ingradientIDName.nameEng;
        }
        return string.Empty;
    }
}

public static class Expectation
{
    public static System.Collections.IEnumerator ExpectationTimer(float time, UnityEngine.Events.UnityAction method)
    {
        yield return new WaitForSeconds(time);
        method.Invoke();
    }
}