
using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        public List<int> numberStars;
        public List<bool> isShowSign;
        public List<bool> isOpen;
        public int allStar;
        public SavesYG() { }
        public SavesYG(LevelDataSO levelDataSO)
        {
            numberStars = new();
            isShowSign = new();
            isOpen = new();
            for (int i = 0; i < levelDataSO.Levels.Length; i++)
            {
                numberStars.Add(levelDataSO.Levels[i].numberStars);
                isShowSign.Add(levelDataSO.Levels[i].isShowSign);
                isOpen.Add(levelDataSO.Levels[i].isOpen);
                allStar = levelDataSO.AllStars;
            }
        }

    }
}
