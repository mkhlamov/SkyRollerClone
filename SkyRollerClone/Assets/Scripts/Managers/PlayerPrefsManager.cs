using UnityEngine;

namespace SkyRollerClone
{
    public class PlayerPrefsManager
    {
        private const string LEVEL = "level";
        private const string GEMS = "gems";

        public static void SetLevel(int v)
        {
            PlayerPrefs.SetInt(LEVEL, v);
        }

        public static int GetLevel()
        {
            return PlayerPrefs.GetInt(LEVEL, 0);
        }

        public static void SetGems(int v)
        {
            PlayerPrefs.SetInt(GEMS, v);
        }

        public static int GetGems()
        {
            return PlayerPrefs.GetInt(GEMS, 0);
        }

        public static void Save()
        {
            PlayerPrefs.Save();
        }
    }
}