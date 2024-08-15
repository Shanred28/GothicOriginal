using UnityEngine;

namespace CodeBase.Configs.Level
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/Levels/LevelConfig", order = 1)]
    public class LevelConfig : ScriptableObject
    {
        public string LevelName;
        public Vector3 HeroSpawnPoint;
    }
}

