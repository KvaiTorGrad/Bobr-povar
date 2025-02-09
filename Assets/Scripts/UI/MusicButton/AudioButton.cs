using UnityEngine;
using UnityEngine.UI;
using YG;

public class AudioButton : MonoBehaviour
{
    private enum ButtonSpecifical
    {
        Music,
        SFX
    }
    [SerializeField] private ButtonSpecifical _buttonSpecifical;
    private Button _button;
    [SerializeField] private Image _img;
    private bool _isMute;
    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(NewSetting);
    }
    private void Start()
    {
        CheckSetting();
    }

    private void CheckSetting()
    {
        if (_buttonSpecifical == ButtonSpecifical.Music)
            _isMute = Settings.Instance.IsMute(0);
        else
            _isMute = Settings.Instance.IsMute(1);
        SetIconColor();
    }
    private void NewSetting()
    {
        YandexGame.FullscreenShow();
        _isMute = !_isMute;
        if (_buttonSpecifical == ButtonSpecifical.Music)
            Settings.Instance.ActiveOreDisActive(_isMute, 0);
        else
            Settings.Instance.ActiveOreDisActive(_isMute, 1);
        SetIconColor();
        Settings.Instance.PlayOneShotClip(0);
    }
    private void SetIconColor()
    {
        if (_isMute)
            _img.color = Color.black;
        else
            _img.color = Color.white;
    }
}
