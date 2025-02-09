using UnityEngine;
using YG;

public class Settings : SingleTon.SingletonBase<Settings>
{
    [SerializeField] private AudioSource _music, _sfx;
    [SerializeField] private AudioClip[] clips;
    private void Start()
    {
        YandexGame.StickyAdActivity(true);
    }

    public bool IsMute(int indexAudio)
    {
        switch (indexAudio)
        {
            case 0:
              return  _music.mute;
            case 1:
              return  _sfx.mute;
        }
        return false;
    }
    public void ActiveOreDisActive(bool isActive, int indexAudio)
    {
        switch (indexAudio)
        {
            case 0:
                _music.mute = isActive;
                break;
            case 1:
                _sfx.mute = isActive;
                break;
        }
    }
    public void PlayOneShotClip(int indexClip) => _sfx.PlayOneShot(clips[indexClip]);

    public void StopClip() => _sfx.Stop();
}
