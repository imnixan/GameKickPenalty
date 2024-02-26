using UnityEngine;

namespace SportsRules
{
    [CreateAssetMenu(fileName = "AppSettings", menuName = "AppSet")]
    public class SettingStrings : ScriptableObject
    {
        public string RulesScene;
        public string GameMainEnterScene;
        public string Save;
        public ScreenOrientation Screen;
        public string Loads;
    }
}
