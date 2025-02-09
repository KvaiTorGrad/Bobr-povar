using YG;

namespace YG.Example
{
    public static class SaveGame
    {
        public static ISaveSystem SaveSystem;
        public static void Save()
        {
            var data = new SavesYG(GameManager.Instance.LevelData);
            SaveSystem.Save(data);
        }

        public static void Load()
        {
            SavesYG data = SaveSystem.Load();
            if (data.numberStars == null)
            {
                LevelCreator.Instance.LevelData.ResetData();
                return;
            }
            LevelCreator.Instance.LevelData.AllStars = data.allStar;
            for (int i = 0; i < LevelCreator.Instance.LevelData.Levels.Length; i++)
            {
                LevelCreator.Instance.LevelData.Levels[i].numberStars = data.numberStars[i];
                LevelCreator.Instance.LevelData.Levels[i].isOpen = data.isOpen[i];
                LevelCreator.Instance.LevelData.Levels[i].isShowSign = data.isShowSign[i];
            }
        }
    }
}
public interface ISaveSystem
{
    public void Save(SavesYG data) { }
    public SavesYG Load();
}
public class YandexGameSaveSystem : ISaveSystem
{
    public void Save(SavesYG data)
    {
        YandexGame.savesData.numberStars = data.numberStars;
        YandexGame.savesData.isOpen = data.isOpen;
        YandexGame.savesData.isShowSign = data.isShowSign;
        YandexGame.savesData.allStar = data.allStar;
        YandexGame.SaveProgress();
    }
    public SavesYG Load() => YandexGame.savesData;
}