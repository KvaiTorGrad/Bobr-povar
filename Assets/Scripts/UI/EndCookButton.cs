using UnityEngine;
using UnityEngine.UI;
using YG;

public class EndCookButton : MonoBehaviour
{
    private Button _button;
    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(EndCook);
    }

    private void EndCook()
    {
        GameManager.Instance.EndCooking(out bool _isEndCook);
        if (_isEndCook)
            _button.interactable = false;
        Settings.Instance.PlayOneShotClip(0);
        YandexGame.FullscreenShow();
    }
}
